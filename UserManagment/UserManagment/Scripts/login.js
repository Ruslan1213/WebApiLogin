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




var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
var CLIENTID = '600271927419-k1c5vd26ogegrtcjdpg49knha8037c5g.apps.googleusercontent.com';
var REDIRECT = 'https://localhost:44303';
var LOGOUT = 'https://localhost:44303';
var TYPE = 'token';
var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;

var acToken;
var tokenType;
var expiresIn;
var user;
var loggedIn = false;

function login() {

    var win = window.open(_url, "windowname1", 'width=800, height=600');
    var pollTimer = window.setInterval(function () {
        try {
            console.log(win.document.URL);
            if (win.document.URL.indexOf(REDIRECT) != -1) {
                window.clearInterval(pollTimer);
                var url = win.document.URL;
                acToken = gup(url, 'access_token');
                tokenType = gup(url, 'token_type');
                expiresIn = gup(url, 'expires_in');

                win.close();
                validateToken(acToken);
            }
        }
        catch (e) {
            console.log(e);
        }
    }, 500);
}

function gup(url, name) {
    namename = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\#&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(url);
    if (results == null)
        return "";
    else
        return results[1];
}

function validateToken(token) {
    getUserInfo();
    $.ajax(
        {
            url: VALIDURL + token,
            data: null,
            success: function (responseText) {
            },
        });
}


async function getUserInfo() {
    $.ajax({

        url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
        success: async function (resp) {
            user = resp;
            user.roleId = 1;
            user.password = user.email;
            user.confirmPassword = user.email;
            await $.post('https://localhost:44303/api/login', user);
            await loginPerson(user);
        }
    })
}

function loginPerson(person) {

    let user = new Object();

    user.userName = person.name;
    user.password = person.email;
    user.roleId = 1;
    user.grant_type = "password";

    $.post('https://localhost:44303/token', user)
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