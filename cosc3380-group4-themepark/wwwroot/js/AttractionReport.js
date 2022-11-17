window.addEventListener("load", (event) =>
{
    const params = new URLSearchParams(window.location.search);
    const year = params.get("year");
    let option = document.getElementById(year);
    option.defaultSelected = true;
})