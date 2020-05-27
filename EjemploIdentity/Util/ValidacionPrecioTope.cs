using EjemploIdentity.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EjemploIdentity
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class PriceRangeAttribute : ValidationAttribute
    {
        private readonly Contexto db = new Contexto();
        private string atributoProducto;
        private double valorMinimo = 0;
        private double valorMaximo = 0;

        public PriceRangeAttribute(string atributoProducto) {
            this.atributoProducto = atributoProducto;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            //Aquí tenemos que usar otra función para pasar el contexto de validación y usar reflection

            int? productoId = getProductoId(validationContext);

            if(productoId==null) return ValidationResult.Success;

            //IEnumerable<ProductoValor> topes = db.ProductoValores.Where(x => x.ProductoId == productoId && x.FechaRegistro == db.ProductoValores.Max(y => y.FechaRegistro)).toList();

            ProductoValor topes0 = db.ProductoValores.Where(x => x.ProductoId == productoId && x.ValorMaximo > 0).OrderByDescending(x => x.FechaRegistro).FirstOrDefault();
            //ProductoValor topes1 = db.ProductoValores.Where(x => x.ProductoId == productoId && x.FechaRegistro == db.ProductoValores.Max(y => y.FechaRegistro)).SingleOrDefault();
            //ProductoValor topes2 = db.ProductoValores.Where(x => x.ProductoId == productoId).Last();
            //ProductoValor topes3 = db.ProductoValores.Where(x => x.ProductoId == productoId).OrderByDescending(x => x.FechaRegistro).FirstOrDefault();
            //ProductoValor topes4 = db.ProductoValores.Where(x => x.ProductoId == productoId).LastOrDefault();

            if (topes0 == null) return ValidationResult.Success;
            if (topes0.ID <=0) return ValidationResult.Success;

            var valor = Convert.ToDouble(value);

            if (valor >= topes0.ValorMinimo && valor <= topes0.ValorMaximo) return ValidationResult.Success;

            this.valorMinimo = topes0.ValorMinimo;
            this.valorMaximo = topes0.ValorMaximo;
            this.ErrorMessage = FormatErrorMessage(validationContext.DisplayName);
            ValidationResult validacionError = new ValidationResult(this.ErrorMessageString, new string[] { validationContext.MemberName });
            
            return validacionError;
        }

        public override string FormatErrorMessage(string name) {
            return string.Format("El campo {0} debe estar entre {1:C} y {2:C}", name, valorMinimo, valorMaximo);
        }

        private int? getProductoId(ValidationContext validationContext) {
            var propertyInfo = validationContext.ObjectType.GetProperty(atributoProducto);
            if (propertyInfo != null)
            {
                var productoId = propertyInfo.GetValue(validationContext.ObjectInstance, null);
                return productoId as int?;
            }
            return -1;
        }
    }
}