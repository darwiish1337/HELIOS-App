const submitForm = document.getElementById("submitForm");
submitForm.addEventListener("submit", (a) => {
    console.log(a);
    a.preventDefault();

    const newPassword = document.getElementById('newPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;


    fetch('https://muhameddarwish-001-site1.ftempurl.com/api/Account/ConfirmResetPassword', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ newPassword, confirmPassword }),
    })
        .then((res) => {
            console.log(res);
            if (res.ok) {
                return res.json();
            } else {
                throw new Error("Invalid email or password");
            }

        })
        .then(data => {
            console.log(localStorage.getItem(data, "token"));
            console.log(localStorage.getItem("email"));
            alert(data, " successful")
            console.log('Success:');
        })
        .catch(error => {
            console.error('Error:', error);
        });
});