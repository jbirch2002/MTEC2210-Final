using Godot;
using System;

public partial class InteractionComponent : Node
{
    [Export] NodePath interactionRaycastPath;
    [Export] NodePath interactionLabelPath;
    [Export] NodePath interactionAudioPath;
    [Export] NodePath interactionCrosshairPath;
    RayCast3D interactionRaycast;
    Label interactionLabel;
    AudioStreamPlayer3D interactionAudio;
    CenterContainer centerContainer;
    
    bool isReset = true;

    public override void _Ready()
    {
        interactionRaycast = GetNode<RayCast3D>(interactionRaycastPath);
        interactionLabel = GetNode<Label>(interactionLabelPath);
        interactionAudio = GetNode<AudioStreamPlayer3D>(interactionAudioPath);
        centerContainer = GetNode<CenterContainer>(interactionCrosshairPath);  
        centerContainer.Visible = false;
    }

    public override void _Process(double delta)
    {
        if (interactionRaycast.IsColliding())
        {
            var interactable = interactionRaycast.GetCollider() as Node;
            isReset = false;
            if(interactable.IsInGroup("Doors"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "Use door\n[E]";
                    centerContainer.Visible = true;
                }
            }
            if(interactable.IsInGroup("EntranceDoorPlayerHouse") || interactable.IsInGroup("EntranceDoorHouse2"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "Enter\n[E]";
                    centerContainer.Visible = true;
                }
            }

            if(interactable.IsInGroup("ExitDoorPlayerHouse") || interactable.IsInGroup("ExitDoorHouse2"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "Leave?\n[E]";
                    centerContainer.Visible = true;
                }
            }
            if(interactable.IsInGroup("ShaftEntrance"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "Enter...?\n[E]";
                    centerContainer.Visible = true;
                }
            }
            if (interactable.IsInGroup("Oven") || interactable.IsInGroup("Cupboard") || interactable.IsInGroup("Fridge"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "Open\n[E]";
                    centerContainer.Visible = true;
                }
            }
            if(interactable.IsInGroup("Bones"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "[E]";
                    centerContainer.Visible = true;
                }
            }
            if(interactable.IsInGroup("Bell"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "Ring?\n[E]";
                    centerContainer.Visible = true;
                }
            }
            if(interactable.IsInGroup("Toilet"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "Flush\n[E]";
                    centerContainer.Visible = true;
                }
            }
            if(interactable.IsInGroup("Lever"))
            {
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactionLabel.Text = "Pull Lever\n[E]";
                    centerContainer.Visible = true;
                }
            }
        }
        else
        {
            if (!isReset)
            {
                interactionLabel.Text = "";
                centerContainer.Visible = false;
                isReset = true;
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("interact"))
        {
            if (interactionRaycast.IsColliding())
            {
                var interactable = interactionRaycast.GetCollider() as Node;
                if (interactable != null && interactable.HasMethod("Interact"))
                {
                    interactable.Call("Interact");
                }
            }
        }
    }
}