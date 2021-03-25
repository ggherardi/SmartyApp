using System;
using System.Collections.Generic;
using System.Text;

namespace Smarty.Models
{
    public interface IStorableItem
    {
        bool EqualsToIdentifier(object identifier);
    }
}
