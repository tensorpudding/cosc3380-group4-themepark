// window.addEventListener("load", (event) =>
// {
//     const params = new URLSearchParams(window.location.search);
//     const year = params.get("year");
//     let option = document.getElementById(year);
//     option.defaultSelected = true;
// })

const disableItemizedForm = () =>
{
    byYearCheck = document.getElementsByName("checkyearorrange")[0];
    byRangeCheck = document.getElementsByName("checkyearorrange")[1];
    startDateInput = document.getElementById("reportStartDate");
    endDateInput = document.getElementById("reportEndDate");
    yearInput = document.getElementById("reportYear");
    if (byYearCheck.checked)
    {
        console.log("We have it set by year");
        startDateInput.disabled = true;
        startDateInput.required = false;
        endDateInput.disabled = true;
        endDateInput.required = false;
        yearInput.disabled = false;
        yearInput.required = true;
    }
    else
    {
        console.log("We have it set by range");
        startDateInput.disabled = false;
        startDateInput.required = true;
        endDateInput.disabled = false;
        endDateInput.required = true;
        yearInput.disabled = true;
        yearInput.required = false;
    }
}

document.getElementsByName("checkyearorrange")[0].addEventListener("change", disableItemizedForm);
document.getElementsByName("checkyearorrange")[1].addEventListener("change", disableItemizedForm);

document.getElementById("generate-report-button").addEventListener('click', () => {
    let check = document.getElementsByName("checkyearorrange")[0].checked;
    var choice;
    if (check)
    {
        choice = "year";
    }
    else
    {
        choice = "range"
    }
    let year = document.getElementById("reportYear").value;
    let startdate = document.getElementById("reportStartDate").value;
    let enddate = document.getElementById("reportEndDate").value;
    let partialString = "/AttractionReport?handler=AttractionReport&checkyearorrange=" + choice + "&year=" + year + "&startdate=" + startdate + "&enddate=" + enddate;
    console.log(partialString);
    fetch(partialString)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            document.getElementById("attraction-report-container").innerHTML = result;
        });
})