var tokenKey = "accessToken"

fetch('/api/roles')
    .then((resp) => resp.json())
    .then(function (data) {
        let selectList = document.getElementById('roles');
        for (let i = 0; i < data.length; i++) {
            selectList.add(new Option(text = data[i].Name, value = data[i].Id));
        }
    });

function ajaxRequest() {

    let person = new Object();

    let form = document.getElementById("f");
    person.userName = form.elements[1].value;
    person.password = form.elements[2].value;
    person.roleId = form.elements[3].value;
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