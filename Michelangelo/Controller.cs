using System.Numerics;
using Silk.NET.Input;

namespace Michelangelo.UI.Controllers;
public class Game : IController, IAgents<IController>
{
    public delegate void AxisTick(Vector3 deltaDirection);
    public AxisTick axisTick;
    int x = 0, y = 0, z = 0;
    public IEnumerable<IController> agents => [this];

    public void OnInput(InputData inputData)
    {
        if (inputData is KeyInputData keyInputData)
        {
            int x = 0, y = 0, z = 0;

            if (keyInputData.key == Key.A)
            {
                x = -1;
            }
            if (keyInputData.key == Key.D)
            {
                x = 1;
            }
            if (keyInputData.key == Key.S)
            {
                y = -1;
            }
            if (keyInputData.key == Key.W)
            {
                y = 1;
            }
            if (keyInputData.key == Key.Q)
            {
                z = -1;
            }
            if (keyInputData.key == Key.E)
            {
                z = 1;
            }
            if (keyInputData.type == KeyInputType.Up)
            {
                x *= -1; y *= -1; z *= -1;
            }


            this.x += x; this.y += y; this.z += z;
        }
    }
    public void Tick(float deltaTime)
    {
        axisTick(new Vector3(x, y, z) * deltaTime);
    }
}