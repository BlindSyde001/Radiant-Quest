using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : InteractableController
{
    [SerializeField] private bool _isOpen;
    [SerializeField] private Sprite gateClosedSprite;
    [SerializeField] private Sprite gateOpenedSprite;

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OpenGate(bool open) {
        _isOpen = open;

        if(_isOpen)
            spriteRenderer.sprite = gateOpenedSprite;
        else
            spriteRenderer.sprite = gateClosedSprite;
    }

    protected override void Interaction() {
        if(_interactionActive) {
            OpenGate(!_isOpen);
        } else {
            throw new InvalidActionException($"You can't open {name} yet!");
        }
    }
}
