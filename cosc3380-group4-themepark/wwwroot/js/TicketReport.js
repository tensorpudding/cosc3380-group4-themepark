const get_numbers_by_day_by_type = (yearNumbers, type) => {
    var classNums = [0, 0, 0, 0, 0, 0, 0];
    var i = 0;
    yearNumbers["totals_by_weekday"].forEach(day => {
        Object.entries(day).forEach(tclass => {
            classNums[i] += tclass[1];
        });
        i++;
    });
    i = 0;
    for (i = 0; i < 7; i++)
    {
        console.log("Day " + i + " sales are " + classNums[i]);
    }
    return classNums;
};

const displayCharts = () => {
    let _ = null;
    const mainChart1 = document.getElementById("main1-chart").getContext("2d");
    const mainChart2 = document.getElementById("main2-chart").getContext("2d");
    var thisYearNumbers = JSON.parse(document.currentScript.getAttribute("weekday_breakdown_this_year"));
    var lastYearNumbers = JSON.parse(document.currentScript.getAttribute("weekday_breakdown_last_year"));

    _ = new Chart(mainChart1,
        {
            type: 'bar',
            data: {
                labels: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                datasets: [{
                    label: 'Poor ticket sales',
                    data: get_numbers_by_day_by_type(thisYearNumbers, 'Poor'),
                    backgroundColor: [
                        // 7 different colors?
                    ],
                    borderColor: [
                        // 7 slightly different colors
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
}
displayCharts();