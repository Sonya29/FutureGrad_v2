function navBarShowLogIn() {
    document.getElementById("logIcon").className = "glyphicon glyphicon-log-in";
    document.getElementById("logText").innerHTML = "Login";
    document.getElementById("logLink").href = "#loginPanel";
}
function navBarShowLogOut() {
    document.getElementById("logIcon").className = "glyphicon glyphicon-log-out";
    document.getElementById("logText").innerHTML = "Logout";
    document.getElementById("logLink").href = "";
    hideHomeMenuItems();
}
function hideHomeMenuItems() {
    var items = document.getElementsByClassName("homeMenuItems");
    for (var i = 0; i < items.length; i++) {
        document.getElementById(items[i].id).style.display = "none";
    }
}
function hideDisabledLinks() {
    var items = document.getElementsByTagName("Button");
    for (var i = 0; i < items.length; i++) {
        if (items[i].id != undefined) {
            if (items[i].id != "") {
                if (document.getElementById(items[i].id).disabled == true) {
                    document.getElementById(items[i].id).style.display = "none";
                }
            }
            
        }
        
    }
}
function displayFileLink(btnFile_id) {
    var filepath = document.getElementById("btnFile_" + btnFile_id).value;
    var arr = filepath.split("\\");
    var filename = arr[arr.length - 1];
    document.getElementById("fileSelected_"+btnFile_id).innerHTML = filename;
}