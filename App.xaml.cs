namespace Calculadora;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    // Crea la ventana principal con la página de la calculadora
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new MainPage())
        {
            Title = "Calculadora",
            Width = 420,
            Height = 600
        };
    }
}
