using CitizenFX.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace DevJacob.IndestructibleObjects.Client
{
    public class Main : ClientScript
    {
        private IEnumerable<Model> FrozenObjectModels = new List<Model>() { };

        public Main()
        {
            string configuredModels = GetResourceMetadata(GetCurrentResourceName(), "indestructible_models", 0);
            FrozenObjectModels = configuredModels.Split(';').Select(modelName => new Model(modelName));
            configuredModels = null;
        }

        [Tick]
        private async Task OnTickHalfSecond()
        {
            await Delay(500);

            World.GetAllProps().ToList().ForEach(prop =>
            {
                try
                {
                    if (prop != null && FrozenObjectModels.Contains(prop.Model))
                    {
                        if (!prop.IsPositionFrozen)
                        {
                            prop.IsPositionFrozen = true;
                            SetEntityCanBeDamaged(prop.Handle, false);
                        }
                    }
                }
                catch { }
            });

            await Task.FromResult(0);
        }
    }
}
