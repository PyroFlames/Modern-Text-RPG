using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public class Item
    {
        public enum ItemTier { Common, Uncommon, Rare, Epic, Chaotic, Legendary, Mythical }

        public enum ItemType { Weapon, Armor, Consumable, Material }
        public enum ArmorType { Helmet, Chestplate, Leggings, Boots, Ring }
        public enum WeaponType { Sword, Bow, Staff }

        public enum DamageType { Melee, Ranged, Magic }

        public string name { get; private set; }
        public string displayName { get; private set; }
        public string description { get; private set; }
        public bool IsMaterial { get; private set; }
        public ItemTier tier { get; private set; }
        public int baseCost { get; private set; }

        public List<DamageType> DamageTypes = new List<DamageType>();
        public WeaponType weaponType { get; private set; }
        public ArmorType armorType { get; private set; }
        public Effect ItemEffect { get; private set; }
        public ItemType itemType { get; private set; }

        public Item(Dictionary<string, string> stringValues, Dictionary<string, int> intvalues, ItemType type, bool ismat, ItemTier itemtier)
        {
            name = stringValues["name"];
            displayName = stringValues["displayname"];
            description = stringValues["itemdescription"];
            baseCost = intvalues["basecost"];
            IsMaterial = ismat;
            tier = itemtier;
            itemType = type;

            AddItemToGameData();
        }
        public Item(Dictionary<string, string> stringValues, Dictionary<string, int> intvalues, ItemType type, bool ismat, ItemTier itemtier, ArmorType aType, Effect iEffect)
        {
            name = stringValues["name"];
            displayName = stringValues["displayname"];
            description = stringValues["itemdescription"];
            baseCost = intvalues["basecost"];
            IsMaterial = ismat;
            tier = itemtier;
            itemType = type;

            AddItemToGameData();
        }
        public Item(Dictionary<string, string> stringValues, Dictionary<string, int> intvalues, ItemType type, bool ismat, ItemTier itemtier, WeaponType wType, List<DamageType> dTypes, Effect iEffect)
        {
            name = stringValues["name"];
            displayName = stringValues["displayname"];
            description = stringValues["itemdescription"];
            baseCost = intvalues["basecost"];
            IsMaterial = ismat;
            tier = itemtier;
            itemType = type;

            AddItemToGameData();
        }
        public Item(Dictionary<string, string> stringValues, Dictionary<string, int> intvalues, ItemType type, bool ismat, ItemTier itemtier, Effect iEffect)
        {
            name = stringValues["name"];
            displayName = stringValues["displayname"];
            description = stringValues["itemdescription"];
            baseCost = intvalues["basecost"];
            IsMaterial = ismat;
            tier = itemtier;
            itemType = type;

            AddItemToGameData();
        }

        private void AddItemToGameData()
        {
            if (MainThread.ItemDictionary.ContainsKey(name))
            {
                Debug.Log("^'Duplicate Item Found'^", false, Debug.LogStates.Warning);
            }
            else
            {
                MainThread.ItemList.Add(this);
                MainThread.ItemDictionary[name] = this;
            }
        }

        public void InvokeItem()
        {

        }
    }
}
