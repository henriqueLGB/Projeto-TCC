const containe = document.querySelector('.containe'),
    pveye = document.querySelectorAll('.pveye'),
    password = document.querySelectorAll('.password'),
    signup = document.querySelector(".signup-link"),
    logintext = document.querySelector(".login-link");



// code JS para Exibir e Ocultas(show e hide)senha

pveye.forEach(eyeIcone => {
    eyeIcone.addEventListener("click", () => {
        password.forEach(password => {
            if (password.type === "password") {
                password.type = "text";
                pveye.forEach(icone => {
                    icone.classList.replace("uil-eye-slash", "uil-eye");
                })

            }
            else {
                password.type = "password";
                pveye.forEach(icone => {
                    icone.classList.replace("uil-eye", "uil-eye-slash");
                })
            }
        })
    })
})

signup.addEventListener("click", () => {
    containe.classList.add("active");

})
logintext.addEventListener("click", () => {
    containe.classList.remove("active");

})

