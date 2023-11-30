// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/////
$(document).ready(function () {

    $('.btn-total-contatos').click(function () {
        var usuarioId = $(this).attr('usuario-id');
        $.ajax({
            type: 'GET',
            url: '/User/ListarContatosPorUsuario/' + usuarioId,
            success: function (result) {
                $("#listaContatosUsuario").html(result);
                let table3 = new DataTable("#table-listagem-contatos", tableConfig);
                $('#modalContatoUsuario').modal('show');
            }
        });
    })
    $('.btn-fechar-modal-contato').click(() => {
        $('#modalContatoUsuario').modal('hide');
    })
    $('.close-alert').click(() => {
        $('.alert').hide('hide');
    })

});

//É necessario esse codigo para que a 2 e proximas paginas obtenham os mesmos eventos onclick
// https://datatables.net/manual/events 
const tableConfig = {
    "ordering": true,
    "paging": true,
    "searching": true,
    "oLanguage": {
        "sEmptyTable": "Nenhum registro encontrado na tabela",
        "sInfo": "Mostrar _START_ até _END_ (de _TOTAL_ registros)",
        "sInfoEmpty": "Mostrar 0 até 0 (de 0 Registros) ",
        "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
        "sInfoPostFix": "",
        "sInfoThousands": ".",
        "sLengthMenu": "Mostrar _MENU_ registros por pagina",
        "sLoadingRecords": "Carregando...",
        "sProcessing": "Processando...",
        "sZeroRecords": "Nenhum registro encontrado",
        "sSearch": "Pesquisar",
        "oPaginate": {
            "sNext": "Proximo",
            "sPrevious": "Anterior",
            "sFirst": "Primeiro",
            "sLast": "Ultimo"
        },
        "oAria": {
            "sSortAscending": ": Ordenar colunas de forma ascendente",
            "sSortDescending": ": Ordenar colunas de forma descendente"
        }
    },
    "lengthMenu": [4, 8, 12],
    "autoWidth": true,
}

let table1 = new DataTable("#table-contatos", tableConfig);
let table2 = new DataTable("#table-usuarios", tableConfig);

var tableUser = $("#table-usuarios").DataTable();
tableUser.on('draw', function () {
    $('.btn-total-contatos').click(function () {
        var usuarioId = $(this).attr('usuario-id');
        $.ajax({
            type: 'GET',
            url: '/User/ListarContatosPorUsuario/' + usuarioId,
            success: function (result) {
                $("#listaContatosUsuario").html(result);
                let table3 = new DataTable("#table-listagem-contatos", tableConfig);
                $('#modalContatoUsuario').modal('show');
            }
        });
    })
    $('.btn-fechar-modal-contato').click(() => {
        $('#modalContatoUsuario').modal('hide');
    })
});




