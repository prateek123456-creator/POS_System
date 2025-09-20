POS Cart & Billing System – Minimal Doc
/////////////////////////////////////////////////////////////////////////

Framework: ASP.NET Core MVC
///////////////////////////////////////////
Features:

Add/update/remove products in cart

Calculate subtotal, 4% tax, discount (flat/%), payable

Suspend and restore orders

Bill preview and payment confirmation
/////////////////////////////////////////////////////////////////////////////////////////
Models:

Product → Id, Name, Price

CartItem → Product, Quantity, Subtotal
////////////////////////////////////////////////////////////////////////////////////////

Controller Actions:

AddToCart, UpdateQuantity, Remove

ApplyDiscount (flat or %)

Cancel, Suspend, Restore

Bill, Payment
/////////////////////////////////////////////////////////////////////////////////////////
View:

Left panel → Cart table + billing

Right panel → Add product

Discount input with Flat/% option

Buttons: Suspend, Restore, Cancel, Bill, Payment

Workflow:
Add products → Apply discount → Suspend/Restore → Bill → Payment
