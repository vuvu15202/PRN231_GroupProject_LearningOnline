$(document).ready(function () {
            $("#btnRegister").click(function (e) {
                const email = document.getElementById('email').value;
                const username = document.getElementById('username').value;
                const password = document.getElementById('password').value;
                document.getElementById('errorMessageUsername').textContent = '';
                document.getElementById('errorMessageEmail').textContent = '';
                document.getElementById('errorMessagePassword').textContent = '';

                var emailRegex = /^[^\s@]+@[^\s@]+\.(com|vn|net|org|edu|gov|mil)$/;
                var passwordRegex = /^[A-Za-z0-9]{8,}$/;
                //Username
                if (username === '') {
                    document.getElementById('errorMessageUsername').textContent = 'Username Không được để trống';
                }

                if (!emailRegex.test(email)) {
                    document.getElementById('errorMessageEmail').textContent = 'Sai format email';
                }
                if (!passwordRegex.test(password)) {
                    document.getElementById('errorMessagePassword').textContent = 'Password phải ít nhất 8 ký tự, chứa chữ cái hoặc số';
                }
                if (password != '' && email != '' && username != '' && emailRegex.test(email) && passwordRegex.test(password)) {
                    const data = {
                        email: email,
                        password: password,
                        userName: username
                    }
                    var requestOptions = {
                        method: 'POST',
                        headers: {
                            'Content-type': 'application/json'
                        },
                        body: JSON.stringify(data)
                    }
                    fetch('https://localhost:5000/api/auth/register', requestOptions)
                    .then(function (response) {
                        if (response.status == 200) {
                            return response.json(); // Parse the response as JSON
                        } else {
                                alert("This account has already exists!");

                            throw new Error('Request failed with status: ' + response.status);
                        }
                    }).then(function (response) {
                        // Check if a redirect URL is provided in the response
                            if (response.redirectUrl) {
                                window.location.href = response.redirectUrl;
                        } else {
                            // Default redirect if no specific role-based URL is provided
                            //window.location.href = '/home';
                        }
                        console.log(response);
                        //alert('Successful!');

                    });
                }
            });
        
        });