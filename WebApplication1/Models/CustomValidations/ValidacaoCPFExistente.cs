using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using WebApplication1.Controllers;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.CustomValidations
{
    public class ValidacaoCPFExistente : ValidationAttribute, IClientValidatable
    {
        public ValidacaoCPFExistente()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            bool valido = ValidaCPFExistente(value.ToString());
            return valido;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(null),
                ValidationType = "customvalidationcpf"
            };
        }

        public static string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        
        public static bool ValidaCPFExistente(string cpf)
        {
            cpf = RemoveNaoNumericos(cpf);

            if (PessoasController.repositorio.GetAll().Any(p => p.CPF.Replace(".", "").Replace("-", "") == cpf))
            {
                return false;
            }           

            return true;
        }
    }
}