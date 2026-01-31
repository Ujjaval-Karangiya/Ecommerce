<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <h1 class="user"></h1>
    <script>
        let currentuser = JSON.parse(localStorage.getItem("CurrentUser"));
        let user = document.getElementsByClassName("user");
        for (const element of user) {
            element.innerHTML = currentuser[0].name;
        }
    </script>
</body>
</html>