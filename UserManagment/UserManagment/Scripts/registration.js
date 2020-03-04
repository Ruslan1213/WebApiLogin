fetch('/api/roles')
    .then((resp) => resp.json())
    .then(function (data) {
        let selectList = document.getElementById('roles');
        for (let i = 0; i < data.length; i++) {
            selectList.add(new Option(text = data[i].Name, value = data[i].Id));
        }
    });

document.getElementById('createUser')
    .addEventListener('click', async function () {

        let person = new Object();
        person.email = document.forms[0].elements[1].value;
        person.name = document.forms[0].elements[2].value;
        person.password = document.forms[0].elements[3].value;
        person.confirmPassword = document.forms[0].elements[4].value;
        person.roleId = document.forms[0].elements[5].value;

        $.post('https://localhost:44303/api/Register', person)
            .done(function (msg) {
                let dangerText = document.getElementById('dangerText');
                dangerText.innerHTML = "";

                let succes = document.getElementById('succes');
                succes.innerHTML = "";
                succes.innerHTML = "OK";
                let myVar = setTimeout(login(person), 5000);

            })
            .fail(function (xhr) {
                let dangerText = document.getElementById('dangerText');
                dangerText.innerHTML = "";
                dangerText.innerHTML += xhr.responseJSON.Message;
            });
    });

function login(person) {

    person.userName = person.name;
    person.grant_type = "password";

    $.post('https://localhost:44303/token', person)
        .done(function (msg) {
            let dangerText = document.getElementById('dangerText');
            dangerText.innerHTML = "";

            let succes = document.getElementById('succes');
            succes.innerHTML = "";
            succes.innerHTML = "OK";
            sessionStorage.setItem("accessToken", msg.access_token);


            content.innerHTML = "";
            hiddenEllements();

            fetch('/Home/Index')
                .then(res => {
                    return res.text();
                })
                .then(data => {
                    let parser = new DOMParser();
                    let doc = parser.parseFromString(data, 'text/html');
                    content.innerHTML = doc.getElementById("content").innerHTML;

                });

        })
        .fail(function (xhr, status, error) {
            let dangerText = document.getElementById('dangerText');
            dangerText.innerHTML = "";
            dangerText.innerHTML = "Password login or role incorrect";
        });
}