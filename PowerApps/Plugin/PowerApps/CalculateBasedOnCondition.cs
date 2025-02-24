using Microsoft.Xrm.Sdk;
using PowerTips.Plugins.Training;
using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerApps
{
    internal class CalculateBasedOnCondition : PluginBase
    {
        public override void Execute(ContextBase context)
        {
            // schema of field
            bool isConditionMet = context.Target.GetAttributeValue<bool>("logical name of the field");

            // 
            context.Trace($"Value for Show/Hide: {isConditionMet}");

            if (isConditionMet)
            {
                // schema of fields
                int Field1 = context.Target.GetAttributeValue<int>("logical name of the field1");
                bool Field2 = context.Target.GetAttributeValue<bool>("logical name of the field2");
                int Field3 = context.Target.GetAttributeValue<int>("logical name of the field3");

                context.Trace($"Field1: {Field1};Field2: {Field2}; Field3: {Field3}");

                int combineField = Field1 + Field3;

                context.Trace($"combineField: {combineField}");

                context.Target["logical name of the field to update"] = combineField;

                // entity update
                Entity toUpdate = new Entity("EntityName", context.Target.Id);
                toUpdate.Attributes.Add("combineField", combineField.ToString());
                context.Update(toUpdate);
            }
        }
    }
}
