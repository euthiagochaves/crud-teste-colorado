// cliente.js - JavaScript para operações CRUD de clientes
$(document).ready(function () {
    // Carregar lista de clientes
    function carregarClientes() {
        $.ajax({
            url: '/api/clientes',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                renderizarTabelaClientes(data);
            },
            error: function (error) {
                console.error('Erro ao carregar clientes:', error);
                exibirMensagem('Erro ao carregar clientes. Por favor, tente novamente.', 'danger');

                // Mostrar mensagem de "nenhum cliente encontrado" quando houver erro
                var tbody = $('#tabela-clientes tbody');
                tbody.empty();
                tbody.append('<tr><td colspan="6" class="text-center">Nenhum cliente encontrado</td></tr>');
            }
        });
    }

    // Renderizar tabela de clientes
    function renderizarTabelaClientes(clientes) {
        var tbody = $('#tabela-clientes tbody');
        tbody.empty();

        if (!clientes || clientes.length === 0) {
            tbody.append('<tr><td colspan="6" class="text-center">Nenhum cliente encontrado</td></tr>');
            return;
        }

        $.each(clientes, function (i, cliente) {
            var telefones = '';
            if (cliente.telefones && cliente.telefones.length > 0) {
                $.each(cliente.telefones, function (j, telefone) {
                    var status = telefone.ativo ? '' : ' <span class="text-danger">(Inativo)</span>';
                    telefones += '<div>' + telefone.tipoTelefone.descricaoTipoTelefone + ': ' + telefone.numeroTelefone + status + '</div>';
                });
            } else {
                telefones = '<em>Nenhum telefone cadastrado</em>';
            }

            var row = '<tr>' +
                '<td>' + cliente.razaoSocial + '</td>' +
                '<td>' + cliente.nomeFantasia + '</td>' +
                '<td>' + cliente.documento + '</td>' +
                '<td>' + cliente.cidade + '/' + cliente.uf + '</td>' +
                '<td>' + telefones + '</td>' +
                '<td class="text-right">' +
                '<a href="/Clientes/Details/' + cliente.codigoCliente + '" class="btn btn-info btn-sm mr-1"><i class="fas fa-eye"></i></a>' +
                '<a href="/Clientes/Edit/' + cliente.codigoCliente + '" class="btn btn-primary btn-sm mr-1"><i class="fas fa-edit"></i></a>' +
                '<button class="btn btn-danger btn-sm btn-excluir" data-id="' + cliente.codigoCliente + '"><i class="fas fa-trash"></i></button>' +
                '</td>' +
                '</tr>';
            tbody.append(row);
        });

        // Adicionar evento para botões de exclusão
        $('.btn-excluir').click(function () {
            var id = $(this).data('id');
            confirmarExclusao(id);
        });
    }

    // Confirmar exclusão de cliente
    function confirmarExclusao(id) {
        if (confirm('Tem certeza que deseja excluir este cliente?')) {
            excluirCliente(id);
        }
    }

    // Excluir cliente
    function excluirCliente(id) {
        $.ajax({
            url: '/api/clientes/' + id,
            type: 'DELETE',
            success: function () {
                exibirMensagem('Cliente excluído com sucesso!', 'success');
                carregarClientes();
            },
            error: function (error) {
                console.error('Erro ao excluir cliente:', error);
                exibirMensagem('Erro ao excluir cliente. Por favor, tente novamente.', 'danger');
            }
        });
    }

    // Exibir mensagem de alerta
    function exibirMensagem(mensagem, tipo) {
        var alertDiv = $('<div class="alert alert-' + tipo + ' alert-dismissible fade show" role="alert">' +
            mensagem +
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
            '<span aria-hidden="true">&times;</span>' +
            '</button>' +
            '</div>');

        $('#mensagens').append(alertDiv);

        // Auto-fechar após 5 segundos
        setTimeout(function () {
            alertDiv.alert('close');
        }, 5000);
    }

    // Adicionar telefone no formulário
    $('#btn-adicionar-telefone').click(function () {
        var index = $('.telefone-item').length;
        var template = $('#telefone-template').html();
        template = template.replace(/\{index\}/g, index);
        $('#telefones-container').append(template);

        // Adicionar evento para remover telefone
        $('.btn-remover-telefone').last().click(function () {
            $(this).closest('.telefone-item').remove();
        });
    });

    // Função para adicionar telefone existente (usado na edição)
    window.adicionarTelefoneExistente = function (telefone) {
        if (!telefone || !telefone.numeroTelefone || telefone.numeroTelefone.trim() === "") return;

        var index = $('.telefone-item').length;
        var template = $('#telefone-template').html();
        template = template.replace(/\{index\}/g, index);

        $('#telefones-container').append(template);

        var item = $('.telefone-item').last();
        item.find('.telefone-numero').val(telefone.numeroTelefone);
        item.find('.telefone-tipo').val(telefone.codigoTipoTelefone);
        item.find('.telefone-operadora').val(telefone.operadora);
        item.find('.telefone-ativo').prop('checked', telefone.ativo);

        item.find('.btn-remover-telefone').click(function () {
            $(this).closest('.telefone-item').remove();
        });
    };

    // Salvar cliente (create/update)
    $('#form-cliente').submit(function (e) {
        e.preventDefault();
        debugger;

        var isValid = validarFormulario();
        if (!isValid) return;

        var clienteId = $('#CodigoCliente').val();
        var isUpdate = clienteId && clienteId !== '0';
        var url = isUpdate ? '/api/clientes/' + clienteId : '/api/clientes';
        var method = isUpdate ? 'PUT' : 'POST';

        var telefones = [];
        $('.telefone-item').each(function () {
            var numero = $(this).find('.telefone-numero').val();
            var tipoTelefoneId = $(this).find('.telefone-tipo').val();
            var operadora = $(this).find('.telefone-operadora').val();
            var ativo = $(this).find('.telefone-ativo').is(':checked');

            if (numero && tipoTelefoneId) {
                var telefone = {
                    numeroTelefone: numero,
                    codigoTipoTelefone: parseInt(tipoTelefoneId),
                    operadora: operadora,
                    ativo: ativo
                };

                telefones.push(telefone);
            }
        });

        var cliente = {
            razaoSocial: $('#RazaoSocial').val(),
            nomeFantasia: $('#NomeFantasia').val(),
            tipoPessoa: $('input[name="TipoPessoa"]:checked').val(),
            documento: $('#Documento').val(),
            endereco: $('#Endereco').val(),
            complemento: $('#Complemento').val(),
            bairro: $('#Bairro').val(),
            cidade: $('#Cidade').val(),
            cep: $('#CEP').val(),
            uf: $('#UF').val(),
            telefones: telefones
        };
        debugger;
        if (isUpdate) {
            cliente.codigoCliente = parseInt(clienteId);
        }

        $.ajax({
            url: url,
            type: method,
            contentType: 'application/json',
            data: JSON.stringify(cliente),
            success: function () {
                var mensagem = isUpdate ? 'Cliente atualizado com sucesso!' : 'Cliente criado com sucesso!';
                window.location.href = '/Clientes?mensagem=' + encodeURIComponent(mensagem) + '&tipo=success';
            },
            error: function (error) {
                console.error('Erro ao salvar cliente:', error);
                exibirMensagem('Erro ao salvar cliente. Por favor, tente novamente.', 'danger');
            }
        });
    });

    // Validar formulário
    function validarFormulario() {
        var isValid = true;

        // Validar campos obrigatórios
        if (!$('#RazaoSocial').val()) {
            exibirMensagem('A Razão Social é obrigatória', 'danger');
            isValid = false;
        }

        if (!$('#NomeFantasia').val()) {
            exibirMensagem('O Nome Fantasia é obrigatório', 'danger');
            isValid = false;
        }

        if (!$('#Documento').val()) {
            exibirMensagem('O Documento é obrigatório', 'danger');
            isValid = false;
        }

        if (!$('#Endereco').val()) {
            exibirMensagem('O Endereço é obrigatório', 'danger');
            isValid = false;
        }

        if (!$('#Bairro').val()) {
            exibirMensagem('O Bairro é obrigatório', 'danger');
            isValid = false;
        }

        if (!$('#Cidade').val()) {
            exibirMensagem('A Cidade é obrigatória', 'danger');
            isValid = false;
        }

        if (!$('#CEP').val()) {
            exibirMensagem('O CEP é obrigatório', 'danger');
            isValid = false;
        }

        if (!$('#UF').val()) {
            exibirMensagem('A UF é obrigatória', 'danger');
            isValid = false;
        }

        // Validar pelo menos um telefone
        if ($('.telefone-item').length === 0) {
            exibirMensagem('Adicione pelo menos um telefone', 'danger');
            isValid = false;
        }

        return isValid;
    }

    // Verificar se estamos na página de listagem para carregar os clientes
    if ($('#tabela-clientes').length > 0) {
        carregarClientes();
    }

    // Verificar se há mensagem na URL para exibir
    var urlParams = new URLSearchParams(window.location.search);
    var mensagem = urlParams.get('mensagem');
    var tipo = urlParams.get('tipo');
    if (mensagem && tipo) {
        exibirMensagem(mensagem, tipo);
    }

    // Adicionar primeiro telefone automaticamente em formulários vazios
    if ($('#form-cliente').length > 0 && $('#telefones-container').children().length === 0) {
        $('#btn-adicionar-telefone').click();
    }
});

