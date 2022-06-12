using System.Threading.Tasks;
using Dapr.Actors;

namespace EasyAbp.EShop.Plugins.Inventories.DaprActors;

public interface IInventoryActor : IActor
{
    Task<InventoryStateModel> GetInventoryStateAsync();

    Task IncreaseInventoryAsync(int quantity, bool decreaseSold);

    Task ReduceInventoryAsync(int quantity, bool increaseSold);
}