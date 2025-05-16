// Arquivo JavaScript global para o site
// Você pode adicionar código JavaScript global aqui

// Exemplo: Formatação de CPF/CNPJ
function formatarDocumento(documento) {
    // Remove caracteres não numéricos
    documento = documento.replace(/\D/g, '');

    // Formata CPF: 000.000.000-00
    if (documento.length === 11) {
        return documento.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
    }

    // Formata CNPJ: 00.000.000/0000-00
    else if (documento.length === 14) {
        return documento.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5');
    }

    // Retorna sem formatação se não for CPF nem CNPJ
    return documento;
}

// Exemplo: Formatação de CEP
function formatarCEP(cep) {
    cep = cep.replace(/\D/g, '');
    return cep.replace(/(\d{5})(\d{3})/, '$1-$2');
}

// Exemplo: Formatação de telefone
function formatarTelefone(telefone) {
    telefone = telefone.replace(/\D/g, '');

    if (telefone.length === 11) {
        return telefone.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
    }
    else if (telefone.length === 10) {
        return telefone.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
    }

    return telefone;
}