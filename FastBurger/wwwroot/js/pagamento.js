document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("checkoutForm");
    const submitButton = form.querySelector("#btnSubmit");

    submitButton.addEventListener("click", function (event) {
        event.preventDefault();

        const ccNome = form.querySelector("#cc-nome").value;
        const ccNumero = form.querySelector("#cc-numero").value;
        const ccExpiracao = form.querySelector("#cc-expiracao").value;
        const ccCvv = form.querySelector("#cc-cvv").value;
        EncriptaCartaoPagSeguro(ccNome, ccNumero, ccExpiracao, ccCvv)

        form.submit();
    });
});

function EncriptaCartaoPagSeguro(ccNome, ccNumero, ccExpiracao, ccCvv) {
    const publicKey = document.querySelector('#publicKey').value;
    let expiracao = ccExpiracao.split('/');

    const card = PagSeguro.encryptCard({
        publicKey: publicKey,
        holder: ccNome,
        number: ccNumero,
        expMonth: expiracao[0],
        expYear: expiracao[1],
        securityCode: ccCvv
    });    

    const resposta = {
        encrypted: card.encryptedCard,
        hasErrors: card.hasErrors,
        errors: card.errors
    };

    console.log(resposta)
}