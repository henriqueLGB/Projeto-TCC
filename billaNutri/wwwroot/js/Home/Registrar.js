async function cadastrar() {

    if ($('#Email').val() == '') {
        return swal.fire("Atenção !","Você deve preencher o campo e-mail","warning")
    }

    if ($('.password').val() == '') {
        return swal.fire("Atenção !", "Você deve preencher o campo Senha", "warning")
    }

    Swal.fire({
        icon: "info",
        title: 'Você deseja realizar o cadastro ?',
        showCancelButton: true,
        confirmButtonText: 'ok',
    }).then(() => {
        $.ajax({
            method: "POST",
            url: "../Home/Registrar",
            data: $("#frRegistrar").serialize(),
            success: function (data) {
                if (data.status == "success") {

                    swal.fire({
                        icon: "success",
                        title: data.status,
                        text: data.message
                    }).then((result) => {
                        location.reload()
                    })

                } else {
                    swal.fire({
                        icon: "error",
                        title: data.status,
                        text: data.message
                    }).then((result) => {
                        location.reload()
                    })
                }
            }
        })
    })

}