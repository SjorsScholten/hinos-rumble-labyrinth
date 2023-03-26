# ItemContainers

Item containers store items.
Item containers have slots where items can be stored in.
If we add an item to a container, we check for an emtpy slot and add the item to that slot.
if the container does not have an empty slot we can not add the item.

## Inventory

We can put items in the inventory.
We can take items out of the inventory.

If the player is holding an item and performs a "stow_inventory" action, the holded item will be added to the inventory and removed from the item holding.

Put an item from a slot into a container
Take an item from a container and put it into a slot

Swap items between slots.

## Hold Item

The player can hold 1 item.

The player can perform a stow_inventory action where the held item is put into his inventory.
The player can perform a stow_belt action where the held item is put into his belt.