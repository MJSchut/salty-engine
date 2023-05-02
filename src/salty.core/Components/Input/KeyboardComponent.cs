using Microsoft.Xna.Framework.Input;

namespace salty.core.Components.Input
{
    public class KeyboardComponent
    {
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;

        /// <summary>
        ///     Create the easy keyboard helper.
        /// </summary>
        public KeyboardComponent()
        {
            _previousKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        ///     Get if capslock is active.
        /// </summary>
        public bool Capslock => _currentKeyboardState.CapsLock;

        /// <summary>
        ///     Get if numlock is active.
        /// </summary>
        public bool NumLock => _currentKeyboardState.NumLock;

        /// <summary>
        ///     Return if any key is currently down.
        /// </summary>
        public bool AnyKeyPressed => _currentKeyboardState.GetPressedKeys().Length > 0;

        /// <summary>
        ///     Return if any of the alt buttons is down.
        /// </summary>
        public bool AltPressed => _currentKeyboardState.IsKeyDown(Keys.LeftAlt) ||
                                  _currentKeyboardState.IsKeyDown(Keys.RightAlt);

        /// <summary>
        ///     Return if any of the ctrl buttons is down.
        /// </summary>
        public bool CtrPressed => _currentKeyboardState.IsKeyDown(Keys.LeftControl) ||
                                  _currentKeyboardState.IsKeyDown(Keys.RightControl);

        /// <summary>
        ///     Return if any of the shift buttons is down.
        /// </summary>
        public bool ShiftPressed => _currentKeyboardState.IsKeyDown(Keys.LeftShift) ||
                                    _currentKeyboardState.IsKeyDown(Keys.RightShift);

        /// <summary>
        ///     Update keyboard-related events.
        ///     Call this function at the beginning of every Update() frame.
        /// </summary>
        public void Update()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        ///     Return if a keyboard button is currently pressed.
        /// </summary>
        /// <param name="button">Keyboard button to check.</param>
        /// <returns>If keyboard button is pressed.</returns>
        public bool IsKeyDown(Keys button)
        {
            return _currentKeyboardState.IsKeyDown(button);
        }

        /// <summary>
        ///     Return if keyboard button is currently released.
        /// </summary>
        /// <param name="button">Keyboard button to check.</param>
        /// <returns>If keyboard button is released.</returns>
        public bool IsKeyUp(Keys button)
        {
            return _currentKeyboardState.IsKeyUp(button);
        }

        /// <summary>
        ///     Return if a keyboard button was pressed this frame.
        /// </summary>
        /// <param name="button">Keyboard button to check.</param>
        /// <returns>If keyboard button is pressed this frame.</returns>
        public bool PressedThisFrame(Keys button)
        {
            return _currentKeyboardState.IsKeyDown(button) && _previousKeyboardState.IsKeyUp(button);
        }

        /// <summary>
        ///     Return if keyboard button was released this frame.
        /// </summary>
        /// <param name="button">Keyboard button to check.</param>
        /// <returns>If keyboard button is released this frame.</returns>
        public bool ReleasedThisFrame(Keys button)
        {
            return _currentKeyboardState.IsKeyUp(button) && _previousKeyboardState.IsKeyDown(button);
        }
    }
}