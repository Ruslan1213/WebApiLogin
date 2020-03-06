var useraccesstoken = sessionStorage.getItem("accessToken");
getTasks();
async function getTasks() {
    await fetch('/api/job', {
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
        }
    }
    )
        .then(response => response.json())
        .then(async function (data) {

            let tasks = await data;
            let content = document.getElementById('tasksContent');
            content.innerHTML = "";
            for (let i = 0; i < tasks.length; i++) {
                content.innerHTML += '<tr>'
                    + '<td>' + tasks[i].Name + '</td>'
                    + '<td>' + tasks[i].Status + '</td>'
                    + '<td>' + tasks[i].Description + '</td>'
                    + '<td>'
                    + '<button data-action="delete" type="button" class="btn btn-danger" data-deleteId=' + "'" + tasks[i].Id + "'" + ' data-toggle="modal" data-target="#exampleModal">Delete</button>' +
                    '|'
                    + '<button data-action="details" type="button" class="btn btn-success" data-deteils=' + tasks[i].Id + ' data-toggle="modal" data-target="#deteilsModal">Details</button>' +
                    '|'
                    + '<button id="updatebtn" data-action="update" type="button"  class="btn btn-danger" data-deteils=' + tasks[i].Id + ' data-toggle="modal" data-target="#updateModal">Update</button>' +
                    '</td>'
                    + '</tr>'
            }
        })
}

document.getElementById('tasksContent').addEventListener('click', function (event) {
    let target = event.target;

    if (target.dataset.action == 'delete') {
        let button = document.getElementById('deletebtn');
        button.dataset.deleteid = target.dataset.deleteid;
    } else if (target.dataset.action == 'details') {
        Click(target);
    } else if (target.dataset.action == 'update') {
        ClickUpdate(target);
    }

});

document.getElementById('deletebtn').addEventListener('click', async function (event) {
    let target = event.target.dataset.deleteid;
    fetch('/api/job/' + target,
        {
            method: 'delete',
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + useraccesstoken
            }
        })
        .then(function () {
            getTasks();
        });

});

async function Click(target) {
    let taskId = target.dataset.deteils;
    const response = await fetch("/api/job/" + taskId, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + useraccesstoken
        }
    });
    if (response.ok === true) {
        const data = await response.json();
        document.getElementById('detailsContent').innerHTML = "";
        document.getElementById('detailsContent').innerHTML += "<label>Name: </label>" + data.Name + "<br/><br/><label>Status: </label>" + data.Status + "<br /><br/><label>Description: </label>" + data.Description + "<br/><br />" +
            "<label>User name: </label >" + data.UserName + "<br/>";
    }
    else
        console.log("Status: ", response.status);
}


document.getElementById('createJob').addEventListener('click', async function () {
    document.getElementById("formCreate").reset();
    const response = await fetch("/api/users", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + useraccesstoken
        }
    });
    if (response.ok === true) {
        const data = await response.json();
        let userList = document.getElementById('users');
        userList.innerHTML = "";
        for (let i = 0; i < data.length; i++) {
            userList.add(new Option(text = data[i].Name, value = data[i].Id));
        }
    }
    else
        console.log("Status: ", response.status);
});

document.getElementById('createFromForm').addEventListener('click', function () {
    let form = document.getElementById('formCreate');
    let formData = new FormData(form);

    let task = new Object();
    task.Name = formData.get("Name");
    task.Status = formData.get("Status");
    task.Description = formData.get("Description");
    task.userId = formData.get("roleId");

    $.ajax('https://localhost:44303/api/job',
        {
            type: "Post",
            url: 'https://localhost:44303/api/job/',
            data: JSON.stringify(task),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + useraccesstoken
            },
            error: function (response) {
                if (response.responseJSON) {
                    alert(response.responseJSON.Message);
                }
                getTasks();
            }
        });

});

document.getElementById('updateFromForm').addEventListener('click', function () {
    let form = document.getElementById('formUpdate');
    let formData = new FormData(form);

    let task = new Object();
    task.Name = formData.get("Name");
    task.Status = formData.get("Status");
    task.Description = formData.get("Description");
    task.userId = formData.get("roleId");

    $.ajax({
        type: "PUT",
        url: 'https://localhost:44303/api/job/' + formData.get("Id"),
        data: JSON.stringify(task),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + useraccesstoken
        },
        error: function (response) {
            getTasks();
            if (response.responseJSON)
                alert(response.responseJSON.Message);
        },
        done: function (responce) {
            getTasks();
        }

    });
});

async function ClickUpdate(target) {
    document.getElementById('usersupdate').innerHTML = "";
    let usersSelect = document.getElementById('usersupdate');
    const resp = await fetch("/api/users", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + useraccesstoken
        }
    });
    if (resp.ok === true) {
        const data = await resp.json();
        let userList = usersSelect;
        for (let i = 0; i < data.length; i++) {
            userList.add(new Option(text = data[i].Name, value = data[i].Id));
        }
    }

    let id = target.dataset.deteils;
    const response = await fetch("/api/job/" + id, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + useraccesstoken
        }
    });
    if (response.ok === true) {
        const data = await response.json();
        let form = document.getElementById('formUpdate');
        form.elements['Name'].value = data.Name;
        form.elements['Id'].value = data.Id;
        form.elements['Description'].value = data.Description;
        form.elements['Status'].value = data.Status;

        let select = document.getElementById('usersupdate');

        for (let i = 0; i < select.options.length; i++) {
            if (select.options[i].value == data.UserId) {
                let index = select.options[i].index;
                select.selectedIndex = index;

            }
        };
    }

}