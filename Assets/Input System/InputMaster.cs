// GENERATED AUTOMATICALLY FROM 'Assets/Input System/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""ef5c4404-6b5a-470c-a7e7-97fc384338fe"",
            ""actions"": [
                {
                    ""name"": ""Walk"",
                    ""type"": ""Value"",
                    ""id"": ""b7142e95-cae1-4e60-9461-a077a4d31ba6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Value"",
                    ""id"": ""a93ac0b5-5265-44c9-b653-db040190a7fd"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pickup"",
                    ""type"": ""Button"",
                    ""id"": ""be304a0d-3bce-4353-a0fa-f11d70a1564c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swap"",
                    ""type"": ""Button"",
                    ""id"": ""87b67b47-267f-4188-9cf2-59d15ad038d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""caac3742-c0c3-4855-913a-2bd221b7db9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FinishTest"",
                    ""type"": ""Button"",
                    ""id"": ""d81464f6-83f5-4d05-8cac-30df89e9c488"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Calculate"",
                    ""type"": ""Button"",
                    ""id"": ""5ad5a47b-b860-4ecd-b3d2-ee710c98742a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""6ce5b946-f01c-446c-bf97-e99ba53ef55e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""3f6639ba-712b-4cfb-90eb-4d17cbc96bd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Prev"",
                    ""type"": ""Button"",
                    ""id"": ""353357ac-b426-48ed-a4f4-469329ccd1ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FinishScene"",
                    ""type"": ""Button"",
                    ""id"": ""b6dd77df-14d7-409b-9bf4-db28f8784558"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResumeScene"",
                    ""type"": ""Button"",
                    ""id"": ""24dc2808-fcd3-43a1-9a3b-49b5ad31299a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""fecbcc81-a4d2-44dc-869a-5cbd517ce51e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7f5c635c-bd4f-40a3-89a0-945b399a501d"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d3e8e15-ace5-4a0d-b21e-2319923eb8a9"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""410a2af8-a20a-434e-86f5-3c9f8b6a1a71"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c03b5e0e-7117-4593-89f0-72fc3b0a52be"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad89adb3-21f2-4c2d-9e20-32f186487f59"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8114dfe3-ad83-4101-bb90-5f03ec35a447"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FinishTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c915048-55ed-4db0-b3c9-56f72a27fae8"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Calculate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bca5843-8d77-46b4-8e50-a585bbab0088"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cef3c67-b9a5-43bd-92e5-87c9786e8997"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""116165c8-b9cc-4348-a1c2-aa7d2a826a52"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prev"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3337f98-a74c-4200-ab6e-d33a69f86a51"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FinishScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b62e0f92-4f1e-45f2-bf71-b7b9989536c2"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResumeScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a3b256b-9a87-4c99-ab53-95d68de16a1b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Hold(duration=0.7)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Walk = m_Player.FindAction("Walk", throwIfNotFound: true);
        m_Player_Turn = m_Player.FindAction("Turn", throwIfNotFound: true);
        m_Player_Pickup = m_Player.FindAction("Pickup", throwIfNotFound: true);
        m_Player_Swap = m_Player.FindAction("Swap", throwIfNotFound: true);
        m_Player_Drop = m_Player.FindAction("Drop", throwIfNotFound: true);
        m_Player_FinishTest = m_Player.FindAction("FinishTest", throwIfNotFound: true);
        m_Player_Calculate = m_Player.FindAction("Calculate", throwIfNotFound: true);
        m_Player_Select = m_Player.FindAction("Select", throwIfNotFound: true);
        m_Player_Next = m_Player.FindAction("Next", throwIfNotFound: true);
        m_Player_Prev = m_Player.FindAction("Prev", throwIfNotFound: true);
        m_Player_FinishScene = m_Player.FindAction("FinishScene", throwIfNotFound: true);
        m_Player_ResumeScene = m_Player.FindAction("ResumeScene", throwIfNotFound: true);
        m_Player_Restart = m_Player.FindAction("Restart", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Walk;
    private readonly InputAction m_Player_Turn;
    private readonly InputAction m_Player_Pickup;
    private readonly InputAction m_Player_Swap;
    private readonly InputAction m_Player_Drop;
    private readonly InputAction m_Player_FinishTest;
    private readonly InputAction m_Player_Calculate;
    private readonly InputAction m_Player_Select;
    private readonly InputAction m_Player_Next;
    private readonly InputAction m_Player_Prev;
    private readonly InputAction m_Player_FinishScene;
    private readonly InputAction m_Player_ResumeScene;
    private readonly InputAction m_Player_Restart;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Walk => m_Wrapper.m_Player_Walk;
        public InputAction @Turn => m_Wrapper.m_Player_Turn;
        public InputAction @Pickup => m_Wrapper.m_Player_Pickup;
        public InputAction @Swap => m_Wrapper.m_Player_Swap;
        public InputAction @Drop => m_Wrapper.m_Player_Drop;
        public InputAction @FinishTest => m_Wrapper.m_Player_FinishTest;
        public InputAction @Calculate => m_Wrapper.m_Player_Calculate;
        public InputAction @Select => m_Wrapper.m_Player_Select;
        public InputAction @Next => m_Wrapper.m_Player_Next;
        public InputAction @Prev => m_Wrapper.m_Player_Prev;
        public InputAction @FinishScene => m_Wrapper.m_Player_FinishScene;
        public InputAction @ResumeScene => m_Wrapper.m_Player_ResumeScene;
        public InputAction @Restart => m_Wrapper.m_Player_Restart;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Walk.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWalk;
                @Turn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Pickup.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Pickup.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Pickup.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Swap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwap;
                @Swap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwap;
                @Swap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwap;
                @Drop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @FinishTest.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFinishTest;
                @FinishTest.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFinishTest;
                @FinishTest.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFinishTest;
                @Calculate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCalculate;
                @Calculate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCalculate;
                @Calculate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCalculate;
                @Select.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Next.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                @Next.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                @Next.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                @Prev.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrev;
                @Prev.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrev;
                @Prev.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrev;
                @FinishScene.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFinishScene;
                @FinishScene.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFinishScene;
                @FinishScene.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFinishScene;
                @ResumeScene.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResumeScene;
                @ResumeScene.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResumeScene;
                @ResumeScene.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnResumeScene;
                @Restart.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
                @Pickup.started += instance.OnPickup;
                @Pickup.performed += instance.OnPickup;
                @Pickup.canceled += instance.OnPickup;
                @Swap.started += instance.OnSwap;
                @Swap.performed += instance.OnSwap;
                @Swap.canceled += instance.OnSwap;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @FinishTest.started += instance.OnFinishTest;
                @FinishTest.performed += instance.OnFinishTest;
                @FinishTest.canceled += instance.OnFinishTest;
                @Calculate.started += instance.OnCalculate;
                @Calculate.performed += instance.OnCalculate;
                @Calculate.canceled += instance.OnCalculate;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Next.started += instance.OnNext;
                @Next.performed += instance.OnNext;
                @Next.canceled += instance.OnNext;
                @Prev.started += instance.OnPrev;
                @Prev.performed += instance.OnPrev;
                @Prev.canceled += instance.OnPrev;
                @FinishScene.started += instance.OnFinishScene;
                @FinishScene.performed += instance.OnFinishScene;
                @FinishScene.canceled += instance.OnFinishScene;
                @ResumeScene.started += instance.OnResumeScene;
                @ResumeScene.performed += instance.OnResumeScene;
                @ResumeScene.canceled += instance.OnResumeScene;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_XboxSchemeIndex = -1;
    public InputControlScheme XboxScheme
    {
        get
        {
            if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
            return asset.controlSchemes[m_XboxSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnWalk(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnPickup(InputAction.CallbackContext context);
        void OnSwap(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnFinishTest(InputAction.CallbackContext context);
        void OnCalculate(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnNext(InputAction.CallbackContext context);
        void OnPrev(InputAction.CallbackContext context);
        void OnFinishScene(InputAction.CallbackContext context);
        void OnResumeScene(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
    }
}
