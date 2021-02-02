using HeyEscape.Interactables.GameItems;
using System.Collections.Generic;

namespace HeyEscape.Core.Interfaces
{
    public interface ISearchable
    {
        List<InventoryItem> Search();
        bool IsSearched { get; }
    }
}