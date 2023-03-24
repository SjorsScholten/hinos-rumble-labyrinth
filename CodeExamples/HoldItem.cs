using System;
using UnityEngine;

public class HoldItem {
    private Item _holdItem;

    public void SetHoldItem(Item holdItem) {
        _holdItem = holdItem;
    }

    public void Use() {

    }

    public void Throw() {

    }

    public void Drop() {

    }

    public void Stow() {

    }
}

public interface IUseHandler {
    void HandleUse();
}

public class Weapon {

}

public class Consumable {
    public bool consumedOnUse;

    public void Consume() {

    }
}

public class Throwable {

    public void Throw() {

    }
}