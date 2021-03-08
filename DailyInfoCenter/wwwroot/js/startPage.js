function infoStart() {
    updateTime();
}
function updateTime() {
    const td = document.getElementById("timedisplay");
    const now = new Date();
    let hrs = now.getHours();
    if (hrs < 10) {
        hrs = "0" + hrs;
    }
    let mins = now.getMinutes();
    if (mins < 10) {
        mins = "0" + mins;
    }
    let secs = now.getSeconds();
    if (secs < 10) {
        secs = "0" + secs;
    }
    td.innerHTML = `${hrs}:${mins}:${secs}`
    setTimeout(updateTime, 1000);
}

