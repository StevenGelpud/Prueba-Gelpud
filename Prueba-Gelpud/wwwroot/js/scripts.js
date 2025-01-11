$(document).ready(function () {
    // Cargar la lista de personas al cargar la página
    reloadPersonaList();

    // Abrir modal para crear
    $('#createPersonaBtn').click(function () {
        $('#personaForm')[0].reset();
        $('#modalTitle').text('Crear Persona');
        $('#personaModal').modal('show');
    });

    // Recargar la lista de personas
    function reloadPersonaList() {
        $.get('/Persona/Index', function (data) {
            $('#scripts-list').html(data); // Corregimos el id a scripts-list para inyectar las filas
        });
    }

    // Guardar persona (Crear / Editar)
    $('#personaForm').submit(function (e) {
        e.preventDefault();

        var id = $('#IdPersona').val();
        var action = id ? '/Persona/Edit/' + id : '/Persona/Create';
        var formData = $('#personaForm').serialize();

        console.log('Formulario enviado:');
        console.log('Acción:', action);
        console.log('Datos:', formData);

        $.ajax({
            url: action,
            type: 'POST',
            data: formData,
            success: function () {
                console.log('Solicitud completada con éxito.');
                $('#personaModal').modal('hide');
                reloadPersonaList(); // Recargar la lista de personas
            },
            error: function (xhr, status, error) {
                console.error('Error en la solicitud:', error);
                console.error('Respuesta del servidor:', xhr.responseText);
                alert('Error al guardar');
            }
        });
    });


    // Editar persona
    $(document).on('click', '.edit-btn', function () {
        var id = $(this).data('id');
        console.log('ID de la persona a editar:', id); 

        $.get('/Persona/Edit/' + id, function (data) {
            console.log('Datos recibidos para editar:', data);

            $('#IdPersona').val(data.idPersona);
            $('#Nombres').val(data.nombres);
            $('#Apellidos').val(data.apellidos);
            $('#Identificacion').val(data.identificacion);
            $('#Genero').val(data.genero);
            $('#FechaNacimiento').val(data.fechaNacimiento);
            $('#Contraseña').val(data.contraseña);
            $('#Activo').prop('checked', data.activo);
            $('#modalTitle').text('Editar Persona');
            $('#personaModal').modal('show');

        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error('Error al obtener los datos:', textStatus, errorThrown);
        });
    });

    // Eliminar persona
    $(document).on('click', '.delete-btn', function () {
        var id = $(this).data('id');
        if (confirm('¿Estás seguro de eliminar esta persona?')) {
            $.ajax({
                url: '/Persona/Delete/' + id,
                type: 'POST',
                success: function () {
                    $('#persona-' + id).remove(); // Elimina la fila de la tabla después de eliminar la persona
                },
                error: function () {
                    alert('Error al eliminar');
                }
            });
        }
    });
});

