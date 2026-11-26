using System.Globalization;

namespace Calculadora;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Se ejecuta al presionar cualquiera de los 4 botones de operación
    private void OnOperacionClicked(object? sender, EventArgs e)
    {
        // 1. Validar el primer número
        if (!TryLeerNumero(Numero1Entry.Text, out double numero1))
        {
            MostrarError("Ingrese un primer número válido.");
            return;
        }

        // 2. Validar el segundo número
        if (!TryLeerNumero(Numero2Entry.Text, out double numero2))
        {
            MostrarError("Ingrese un segundo número válido.");
            return;
        }

        // 3. Saber qué operación se pidió (+, -, *, /)
        string operacion = (sender as Button)?.CommandParameter?.ToString() ?? "";

        // 4. Validar la división para cero
        if (operacion == "/" && numero2 == 0)
        {
            MostrarError("No se puede dividir para cero.");
            return;
        }

        // 5. Calcular
        double resultado = operacion switch
        {
            "+" => numero1 + numero2,
            "-" => numero1 - numero2,
            "*" => numero1 * numero2,
            "/" => numero1 / numero2,
            _ => 0
        };

        // 6. Validar que el resultado no sea demasiado grande
        if (!double.IsFinite(resultado))
        {
            MostrarError("El resultado es demasiado grande.");
            return;
        }

        MostrarResultado(resultado);
    }

    // Limpia las cajas de texto y el resultado
    private void OnLimpiarClicked(object? sender, EventArgs e)
    {
        Numero1Entry.Text = string.Empty;
        Numero2Entry.Text = string.Empty;
        ResultadoLabel.Text = "Resultado: ";
        ResultadoLabel.TextColor = Colors.Black;
        Numero1Entry.Focus();
    }

    // Convierte el texto a número. Acepta punto o coma como decimal.
    private static bool TryLeerNumero(string? texto, out double numero)
    {
        numero = 0;

        if (string.IsNullOrWhiteSpace(texto))
            return false;

        texto = texto.Trim().Replace(',', '.');

        return double.TryParse(texto, NumberStyles.Float, CultureInfo.InvariantCulture, out numero)
               && double.IsFinite(numero);
    }

    private void MostrarResultado(double resultado)
    {
        // Redondeo para evitar resultados como 0.30000000000000004
        resultado = Math.Round(resultado, 10);
        ResultadoLabel.TextColor = Colors.Black;
        ResultadoLabel.Text = $"Resultado: {resultado.ToString(CultureInfo.InvariantCulture)}";
    }

    private void MostrarError(string mensaje)
    {
        ResultadoLabel.TextColor = Colors.Red;
        ResultadoLabel.Text = mensaje;
    }
}
