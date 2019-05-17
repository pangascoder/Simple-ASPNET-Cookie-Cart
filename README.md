# ASPNET-Cookie-Cart-v1
Simple ASP.NET Web application demonstrating add/view cart functionality using cookies, DataTable and Dictionaries. The main focus of this project is the CookieCartParser class that contains the functions to manipulate the cart in the cookie and the DataTable view in the webforms.

## What is a CookieCartParser?
The CookieCartParser is a class which contains functions that are used to manipulate a cookie string specifically a CookieCart string. The class contains the following functions:

- Dictionary<string, string> ToDictionary(string strCookie)

- string Add(string newItemID, string strCookie)

- string Remove(string itemIDToRemove, string strCookie)

- string Update(string itemIDToUpdate, string newData, string strCookie)

- string Stringify(Dictionary<string, string> dictCookie)

- int GetNumberOfItems(string strCookie)

You can find more details about these functions inside the [CookieCartParser class](./Add-To-Cart-v1/CookieCartParser.cs).


## Why was CookieCartParser created?
The CookieCartParser class was created for my Web Application Development class. One of the features to be integrated in the web application was the **Add to Cart** functionality. We were also required to store the data from our cart into an *HTTPCookie*.

You can store data into your cookie and retrieve it easily using Request.Cookies\["Name of your cookie"\]. For an Add to Cart functionality, you will need to store the item that was added. Cookies are not ideal for large, secured data so it's best to use cookies sparingly and add bits of information on it. So instead of storing all the details of the item, simply store the ID of the item, which we can use to access the details of the item.

Most CookieCart as string implementation I've seen online simply adds the item ID each time. So if I have two items with IDs 1 and 2 respectively, and the user clicked item 1 three times then item 2 two times then item 1 again five times, the CookieCart string would look like this:

> 1,1,1,2,2,1,1,1,1,

which is not bad. However, if we want to display the quantity of each item, we will need to traverse through this string in a loop. And if we need to delete an item, we will again traverse through a loop to find the instance of that item's ID. 

So this implementation is great when adding to the cart but simply tedious when manipulating data from the cart.

So I thought of using this pattern:
> 1=8,2=2

which basically means, item 1 has been added to the cart 8 times (quantity) and item 2 has been added to the cart 2 times. If the user clicks item 1 or 2 again, it would simply update the number after the '=' character. If the user clicks a third item with ID 3, it would simple update into:
> 1=8,2=2,3=1

It looks easy, right? But accessing the item ID and updating the quantity value with only string manipulation was quite a complex process so I used **Dictionaries** (a.k.a. my favorite Data Structure). To learn more about Dictionaries in C#, click [here](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?redirectedfrom=MSDN&view=netframework-4.8].

So with a Dictionary, I could assign the item ID as the Key and the quantity as the Value. If I need to update the quantity, I can just access it using the item ID. If I need to remove the item, I can just remove it from the Dictionary using the ID. Now, it's easy.

You might be thinking, "*But wait! Cookies are strings, right? How do we make a string into a Dictionary?*"

That's why I have a ToDictionary() function so I can easily manipulate the data. If I need to pass the cookie string, I simply call the Stringify function to convert the Dictionary into a string. You can find the code inside the [CookieCartParser class](./Add-To-Cart-v1/CookieCartParser.cs).

## How To Use

You can check out the */demo* folder to see how to use the CookieCartParser in a simple web application with Cart functionality.
Simple add the CookieCartParser class into your project, create an instance of it inside your source code and viola! You can now manipulate your cookie string.

If you already have an existing web application with add to cart functionality and simply needs a way to store and manage data of an HTTPCookie, just download the [CookieCartParser class](./Add-To-Cart-v1/CookieCartParser.cs). No need to download the entire project.

## Changes
- [x] Added /demo web application
- [x] Added a README file to explain the CookieCartParser
- [ ] Expand on How To Use
- [ ] Make use of Update function in the /demo web application

*Note: When I created this application, I wasn't educated with the latest web technologies. You can use the CookieCartParser if you have limited knowledge about the different data formats (XML, JSON, etc.) and comfortable with simple string manipulation. However, this is probably not the best way to implement Cart functionality using HTTPCookie. It is simple and straightforward, though.
