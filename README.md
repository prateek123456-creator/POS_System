POS Cart and Billing System – Short Documentation

Project: POS_System
Framework: ASP.NET Core MVC

1. Overview

A POS module to:

Add, update, and remove products in a cart

Calculate subtotal, tax (4%), discount (flat or %), and payable amount

Suspend and restore orders

Generate Bill preview and Payment confirmation

2. Models

Product: Id, Name, Price

CartItem: Product, Quantity, Subtotal (Price × Quantity)

3. Controller: CartController
Key Features:

AddToCart: Add or merge products

UpdateQuantity / Remove: Edit or delete products

ApplyDiscount: Apply flat or % discount

Cancel: Clear cart and reset discount

Suspend / Restore: Temporarily pause and resume orders

Bill / Payment: Show bill preview and confirm payment

Variables:

_cart → active cart

_suspendedCart → suspended cart

_discount → current discount

_suspendedDiscount → discount for suspended cart

TaxRate = 0.04m → 4% tax

4. View (Index.cshtml)

Left Panel: Cart table + billing summary

Right Panel: Add product form

Billing Summary: Total Items, Subtotal, Tax, Total, Discount, Payable

Discount Form: Input + Flat/% selection

Actions: Suspend, Restore, Cancel, Bill, Payment

Messages: Bill preview and payment confirmation via TempData

5. Workflow Example

Add products → Cart shows subtotal

Tax and discount applied → Payable calculated

Suspend → temporarily save cart

Restore → retrieve suspended cart

Bill → preview order

Payment → finalize and show confirmation

This short documentation summarizes features, workflow, and structure of your POS system.
