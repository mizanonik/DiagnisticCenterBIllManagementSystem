using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemWebApp.Gateway;
using DiagnosticCenterBillManagementSystemWebApp.Model;

namespace DiagnosticCenterBillManagementSystemWebApp.BLL
{
    public class TypeNameManager
    {
        TypeNameGateway gateway = new TypeNameGateway();
        public string SaveType(string typeName)
        {
            if (typeName!=String.Empty)
            {
                if (gateway.GetSearchedType(typeName)!=null)
                {
                    return "Type Name Already Exists";
                }
                else
                {
                    if (gateway.SaveType(typeName)>0)
                    {
                        return "Type Name saved successfully.";
                    }
                    return "Failed to save type name.";
                }
            }
            return "Please Enter a type Name.";
        }

        public List<TestType> GetAllTestTypes()
        {
            return gateway.GetAllTypeNames();
        } 
    }
}