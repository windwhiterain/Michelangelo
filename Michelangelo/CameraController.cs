using System.Numerics;

namespace Michelangelo.UI.CameraControllers;

class DefualtCameraController : ICameraController
{
    Cameras.DefualtCamera camera = new();
    public ICamera agent => camera;
    Controllers.Game game = new();

    public IEnumerable<IController> agents => game.agents;

    float speed;
    public DefualtCameraController(float speed)
    {
        this.speed = speed;
        game.axisTick = (directionTick) =>
        {
            camera.lookAt.position += speed * directionTick;
        };
    }
}

