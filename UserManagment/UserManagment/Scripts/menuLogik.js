let accessToken = "accessToken";
let content = document.getElementById('content');
let index = document.getElementById('index');
let log = document.getElementById('login');
let logout = document.getElementById('logout');
let register = document.getElementById('register');
let getUser = document.getElementById('getUser');

hiddenEllements();

index.addEventListener('click', function () {
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
});

log.addEventListener('click', function () {
    content.innerHTML = "";
    hiddenEllements();

    fetch('/Home/Login')
        .then(res => {
            return res.text();
        })
        .then(data => {
            let parser = new DOMParser();
            let doc = parser.parseFromString(data, 'text/html');
            content.innerHTML = doc.getElementById("content").innerHTML;

            let scr = document.createElement("script");
            scr.setAttribute("src", "/Scripts/login.js");
            document.innerHTML = "";
            document.getElementById('scripts').appendChild(scr);
            hiddenEllements();
        });
});

logout.addEventListener('click', function () {
    content.innerHTML = "";
    sessionStorage.removeItem(accessToken);
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
});

register.addEventListener('click', function () {
    content.innerHTML = "";
    hiddenEllements();

    fetch('/Home/Main')
        .then(res => {
            return res.text();
        })
        .then(data => {
            let parser = new DOMParser();
            let doc = parser.parseFromString(data, 'text/html');
            content.innerHTML = doc.getElementById("content").innerHTML;
            let scr = document.createElement("script");
            scr.setAttribute("src", "/Scripts/registration.js");
            document.innerHTML = "";
            document.getElementById('scripts').appendChild(scr);
        });
});

getUser.addEventListener('click', async function () {
    content.innerHTML = "";
    hiddenEllements();

    const token = sessionStorage.getItem("accessToken");

    const response = await fetch("/api/users", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });
    if (response.ok === true) {
        const data = await response.json();
        console.log(data);
        for (let i = 0; i < data.length; i++) {

            content.innerHTML += "<hr/><p><h3> Name: " + data[i].Name + ", Mail: " + data[i].Email + "</h3></p>";
        }
    }
    else
        console.log("Status: ", response.status);
});

function hiddenEllements() {
    const token = sessionStorage.getItem(accessToken);
    if (token == null) {
        getUser.style.display = "none";
        logout.style.display = "none";
        log.style.display = "inline";
        register.style.display = "inline";
    }
    else {
        log.style.display = "none";
        register.style.display = "none";
        getUser.style.display = "inline";
        logout.style.display = "inline";
    }
}