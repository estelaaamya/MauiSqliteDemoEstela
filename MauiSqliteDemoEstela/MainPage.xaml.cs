namespace MauiSqliteDemoEstela
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _database;
        private Cliente? _clienteSeleccionado;

        public MainPage(LocalDbService database)
        {
            InitializeComponent();

            _database = database;

            Loaded += async (sender, e) =>
            {
                await CargarClientes();
            };
        }

        // Consultar y mostrar los clientes
        private async Task CargarClientes()
        {
            var clientes = await _database.GetClientes();
            ClientesCollection.ItemsSource = clientes;
        }

        // Guardar un cliente nuevo
        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                string.IsNullOrWhiteSpace(CorreoEntry.Text))
            {
                await DisplayAlert("Aviso",
                    "Ingresa el nombre y el correo.", "Aceptar");
                return;
            }

            var cliente = new Cliente
            {
                Nombre = NombreEntry.Text.Trim(),
                Correo = CorreoEntry.Text.Trim(),
                Telefono = TelefonoEntry.Text?.Trim() ?? ""
            };

            await _database.Create(cliente);

            await DisplayAlert("Éxito",
                "Cliente guardado correctamente.", "Aceptar");

            LimpiarCampos();
            await CargarClientes();
        }

        // Seleccionar un cliente de la lista
        private void Clientes_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Cliente cliente)
            {
                _clienteSeleccionado = cliente;

                NombreEntry.Text = cliente.Nombre;
                CorreoEntry.Text = cliente.Correo;
                TelefonoEntry.Text = cliente.Telefono;
            }
        }

        // Editar un cliente
        private async void Editar_Clicked(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                await DisplayAlert("Aviso",
                    "Selecciona un cliente para editar.", "Aceptar");
                return;
            }

            if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                string.IsNullOrWhiteSpace(CorreoEntry.Text))
            {
                await DisplayAlert("Aviso",
                    "Ingresa el nombre y el correo.", "Aceptar");
                return;
            }

            _clienteSeleccionado.Nombre = NombreEntry.Text.Trim();
            _clienteSeleccionado.Correo = CorreoEntry.Text.Trim();
            _clienteSeleccionado.Telefono = TelefonoEntry.Text?.Trim() ?? "";

            await _database.Update(_clienteSeleccionado);

            await DisplayAlert("Éxito",
                "Cliente actualizado correctamente.", "Aceptar");

            LimpiarCampos();
            await CargarClientes();
        }

        // Eliminar un cliente
        private async void Eliminar_Clicked(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                await DisplayAlert("Aviso",
                    "Selecciona un cliente para eliminar.", "Aceptar");
                return;
            }

            bool confirmar = await DisplayAlert(
                "Confirmar",
                "¿Deseas eliminar este cliente?",
                "Sí",
                "No");

            if (!confirmar)
                return;

            await _database.Delete(_clienteSeleccionado);

            await DisplayAlert("Éxito",
                "Cliente eliminado correctamente.", "Aceptar");

            LimpiarCampos();
            await CargarClientes();
        }

        // Limpiar los campos
        private void Limpiar_Clicked(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            NombreEntry.Text = "";
            CorreoEntry.Text = "";
            TelefonoEntry.Text = "";

            _clienteSeleccionado = null;
            ClientesCollection.SelectedItem = null;
        }
    }
}