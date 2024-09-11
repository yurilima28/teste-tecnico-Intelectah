﻿
$(document).ready(function () {
    $('#FabricanteID').change(function () {
        var fabricanteId = $(this).val();

        if (fabricanteId) {
            $.ajax({
                url: '/Vendas/BuscarPorFabricante',
                type: 'GET',
                data: { fabricanteId: fabricanteId },
                success: function (response) {
                    if (response.sucesso) {
                        var veiculoSelect = $('#veiculoSelect');
                        veiculoSelect.empty();

                        veiculoSelect.append('<option value="">Selecione um modelo</option>');

                        $.each(response.data, function (index, modelo) {
                            veiculoSelect.append($('<option>', {
                                value: modelo.value,
                                text: modelo.text
                            }));
                        });
                    } else {
                        alert(response.mensagem);
                    }
                },
                error: function () {
                    alert('Erro ao buscar os modelos.');
                }
            });
        } else {
            $('#veiculoSelect').empty();
            $('#veiculoSelect').append('<option value="">Selecione um fabricante primeiro</option>');
        }
    });
});
