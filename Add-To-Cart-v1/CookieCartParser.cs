using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Add_To_Cart_v1
{
    /// <summary>
    /// The functions of this class is used to manipulate the cart data stored in a cookie.
    /// </summary>
    public class CookieCartParser
    {
        Dictionary<string, string> dictCookie;

        public CookieCartParser() { }

        /// <summary>
        /// Parses the cookie cart into a Dictionary, making it more accessible
        /// </summary>
        /// <param name="strCookie">cookie string taken from the HTTPCookie with the pattern => id=quantity,id=quantity,..</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, string> ToDictionary(string strCookie)
        {
            dictCookie = new Dictionary<string, string>();

            dictCookie = strCookie.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(part => part.Split('=')).ToDictionary(split => split[0], split => split[1]);

            return dictCookie;
        }

        /// <summary>
        /// Adds new item to the string and returns the newly created string cookie
        /// </summary>
        /// <param name="newItemID">the item to be added to the cookie</param>
        /// <param name="strCookie">cookie string taken from the HTTPCookie</param>
        /// <returns>new cookie string after addition</returns>
        public string Add(string newItemID, string strCookie)
        {
            dictCookie = ToDictionary(strCookie);

            if (dictCookie.Keys.Contains(newItemID))
            {
                int item_quantity = int.Parse(dictCookie[newItemID]);
                item_quantity++;
                dictCookie[newItemID] = item_quantity.ToString();
            }
            else
            {
                dictCookie[newItemID] = "1";
            }

            return Stringify(dictCookie);
        }

        /// <summary>
        /// Removes the item from the cookie
        /// </summary>
        /// <param name="itemIDToRemove"></param>
        /// <param name="strCookie"></param>
        /// <returns>new cookie string after removal</returns>
        public string Remove(string itemIDToRemove, string strCookie)
        {
            dictCookie = ToDictionary(strCookie);

            dictCookie.Remove(itemIDToRemove);

            return Stringify(dictCookie);
        }

        /// <summary>
        /// Updates the item data in the cookie string
        /// </summary>
        /// <param name="itemIDToUpdate">ID of item to update</param>
        /// <param name="newData">new quantity of the item</param>
        /// <param name="strCookie">cookie string</param>
        /// <returns>new cookie string after addition</returns>
        public string Update(string itemIDToUpdate, string newData, string strCookie)
        {
            dictCookie = ToDictionary(strCookie);

            if (dictCookie.Keys.Contains(itemIDToUpdate))
            {
                dictCookie[itemIDToUpdate] = newData;
            }

            return Stringify(dictCookie);
        }

        /// <summary>
        /// Converts the Dictionary cookie into a string
        /// </summary>
        /// <param name="dictCookie">the dictionary containing cookie data</param>
        /// <returns>string equivalent of the cookie</returns>
        public string Stringify(Dictionary<string, string> dictCookie)
        {
            string strCookie = string.Join(",", dictCookie.Select(x => x.Key + "=" + x.Value).ToArray());

            //strCookie += ",";

            return strCookie;
        }

        /// <summary>
        /// Gets the number of items in the cookie
        /// </summary>
        /// <param name="strCookie">cookie string</param>
        /// <returns>number of items</returns>
        public int GetNumberOfItems(string strCookie)
        {
            int count = 0;

            dictCookie = ToDictionary(strCookie);

            foreach (KeyValuePair<string, string> item in dictCookie)
            {
                int item_count = int.Parse(item.Value);

                count += item_count;
            }

            return count;
        }
    }
}