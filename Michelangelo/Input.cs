using System.Numerics;
using Michelangelo.Math;
using Silk.NET.Input;

namespace Michelangelo;
public enum KeyInputType
{
    Down,
    Up,
}
public abstract record class InputData;
public record class KeyInputData
(
    Key key,
    KeyInputType type
) : InputData;
public record class MouseInputData
(
    MouseButton button,
    Float2 position
) : InputData;