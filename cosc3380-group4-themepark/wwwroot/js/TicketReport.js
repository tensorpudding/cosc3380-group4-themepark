const get_numbers_by_day_by_type = (yearNumbers, type) => {
    var classNums = [0, 0, 0, 0, 0, 0, 0];
    var i = 0;
    if (type == "All") {
        yearNumbers["totals_by_weekday"].forEach(day => {
            Object.entries(day).forEach(tclass => {
                classNums[i] += tclass[1];
            });
            i++;
        });
    } else {
        yearNumbers["totals_by_weekday"].forEach(day => {
            classNums[i] = day[type];
            i++;
        });
    }
    i = 0;
    for (i = 0; i < 7; i++)
    {
        console.log("Day " + i + " sales of type " + type + " are " + classNums[i]);
    }
    return classNums;
};

const get_numbers_by_day_by_week = (yearNumbers) => {
    return yearNumbers["totals_by_weekday_for_week"];
};

const displayCharts = () => {
    let _ = null;
    const mainChart1 = document.getElementById("main1-chart").getContext("2d");
    const mainChart2 = document.getElementById("main2-chart").getContext("2d");
    const mainChart3 = document.getElementById("main3-chart").getContext("2d");
    var thisYearNumbersAverage = JSON.parse(document.currentScript.getAttribute("weekday_breakdown_this_year"));
    var lastYearNumbersAverage = JSON.parse(document.currentScript.getAttribute("weekday_breakdown_last_year"));
    var thisYearNumbersThisWeek = JSON.parse(document.currentScript.getAttribute("this_week_this_year"));
    var lastYearNumbersThisWeek = JSON.parse(document.currentScript.getAttribute("this_week_last_year"));

    _ = new Chart(mainChart1,
        {
            type: 'bar',
            data: {
                labels: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                datasets: [
                    {
                    label: 'Poor ticket sales',
                    data: get_numbers_by_day_by_type(thisYearNumbersAverage, 'Poor'),
                    backgroundColor: [
                        // 7 different colors?
                        'rgba(0, 63, 92, 0.6)',
                        'rgba(44, 72, 117, 0.6)',
                        'rgba(138, 80, 143, 0.6)',
                        'rgba(188, 80, 144, 0.6)',
                        'rgba(255, 99, 97, 0.6)',
                        'rgba(255, 133, 49, 0.6)',
                        'rgba(255, 166, 0, 0.6)'
                    ],
                    borderColor: [
                        // 7 slightly different colors
                        'rgba(0, 63, 92, 1)',
                        'rgba(44, 72, 117, 1)',
                        'rgba(138, 80, 143, 1)',
                        'rgba(188, 80, 144, 1)',
                        'rgba(255, 99, 97, 1)',
                        'rgba(255, 133, 49, 1)',
                        'rgba(255, 166, 0, 1)'
                    ],
                    borderWidth: 1
                    },
                    {
                        label: 'Normal ticket sales',
                        data: get_numbers_by_day_by_type(thisYearNumbersAverage, 'Normal'),
                        backgroundColor: [
                            // 7 different colors?
                            'rgba(0, 63, 92, 0.6)',
                            'rgba(44, 72, 117, 0.6)',
                            'rgba(138, 80, 143, 0.6)',
                            'rgba(188, 80, 144, 0.6)',
                            'rgba(255, 99, 97, 0.6)',
                            'rgba(255, 133, 49, 0.6)',
                            'rgba(255, 166, 0, 0.6)'
                        ],
                        borderColor: [
                            // 7 slightly different colors
                            'rgba(0, 63, 92, 1)',
                            'rgba(44, 72, 117, 1)',
                            'rgba(138, 80, 143, 1)',
                            'rgba(188, 80, 144, 1)',
                            'rgba(255, 99, 97, 1)',
                            'rgba(255, 133, 49, 1)',
                            'rgba(255, 166, 0, 1)'
                        ],
                        borderWidth: 1
                    },
                    {
                        label: 'Premium ticket sales',
                        data: get_numbers_by_day_by_type(thisYearNumbersAverage, 'Premium'),
                        backgroundColor: [
                            // 7 different colors?
                            'rgba(0, 63, 92, 0.6)',
                            'rgba(44, 72, 117, 0.6)',
                            'rgba(138, 80, 143, 0.6)',
                            'rgba(188, 80, 144, 0.6)',
                            'rgba(255, 99, 97, 0.6)',
                            'rgba(255, 133, 49, 0.6)',
                            'rgba(255, 166, 0, 0.6)'
                        ],
                        borderColor: [
                            // 7 slightly different colors
                            'rgba(0, 63, 92, 1)',
                            'rgba(44, 72, 117, 1)',
                            'rgba(138, 80, 143, 1)',
                            'rgba(188, 80, 144, 1)',
                            'rgba(255, 99, 97, 1)',
                            'rgba(255, 133, 49, 1)',
                            'rgba(255, 166, 0, 1)'
                        ],
                        borderWidth: 1
                    }
                ]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                },
                responsive: true
            }
        });
    _ = new Chart(mainChart2,
        {
            type: 'pie',
            data: {
                labels: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                datasets: [
                    {
                        label: 'Ticket Sales this year by day of the week',
                        data: get_numbers_by_day_by_type(thisYearNumbersAverage, 'All'),
                        backgroundColor: [
                            // 7 different colors?
                            'rgba(0, 63, 92, 0.6)',
                            'rgba(44, 72, 117, 0.6)',
                            'rgba(138, 80, 143, 0.6)',
                            'rgba(188, 80, 144, 0.6)',
                            'rgba(255, 99, 97, 0.6)',
                            'rgba(255, 133, 49, 0.6)',
                            'rgba(255, 166, 0, 0.6)'
                        ],
                        borderColor: [
                            // 7 slightly different colors
                            'rgba(0, 63, 92, 1)',
                            'rgba(44, 72, 117, 1)',
                            'rgba(138, 80, 143, 1)',
                            'rgba(188, 80, 144, 1)',
                            'rgba(255, 99, 97, 1)',
                            'rgba(255, 133, 49, 1)',
                            'rgba(255, 166, 0, 1)'
                        ],
                        borderWidth: 1,
                        hoverOffset: 4
                    } 
                ]
            },
            options: {
                responsive: true
            }
        });
    _ = new Chart(mainChart3,
        {
            type: 'bar',
            data: {
                labels: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                datasets: [
                    {
                        label: 'Current Ticket Sales This Week',
                        data: get_numbers_by_day_by_week(thisYearNumbersThisWeek),
                        backgroundColor: [
                            // 7 different colors?
                            'rgba(0, 63, 92, 0.6)',
                            'rgba(44, 72, 117, 0.6)',
                            'rgba(138, 80, 143, 0.6)',
                            'rgba(188, 80, 144, 0.6)',
                            'rgba(255, 99, 97, 0.6)',
                            'rgba(255, 133, 49, 0.6)',
                            'rgba(255, 166, 0, 0.6)'
                        ],
                        borderColor: [
                            // 7 slightly different colors
                            'rgba(0, 63, 92, 1)',
                            'rgba(44, 72, 117, 1)',
                            'rgba(138, 80, 143, 1)',
                            'rgba(188, 80, 144, 1)',
                            'rgba(255, 99, 97, 1)',
                            'rgba(255, 133, 49, 1)',
                            'rgba(255, 166, 0, 1)'
                        ],
                        borderWidth: 1,
                        hoverOffset: 4
                    },
                    {
                        label: 'Ticket Sales This Week Last Year',
                        data: get_numbers_by_day_by_week(lastYearNumbersThisWeek),
                        backgroundColor: [
                            // 7 different colors?
                            'rgba(0, 63, 92, 0.6)',
                            'rgba(44, 72, 117, 0.6)',
                            'rgba(138, 80, 143, 0.6)',
                            'rgba(188, 80, 144, 0.6)',
                            'rgba(255, 99, 97, 0.6)',
                            'rgba(255, 133, 49, 0.6)',
                            'rgba(255, 166, 0, 0.6)'
                        ],
                        borderColor: [
                            // 7 slightly different colors
                            'rgba(0, 63, 92, 1)',
                            'rgba(44, 72, 117, 1)',
                            'rgba(138, 80, 143, 1)',
                            'rgba(188, 80, 144, 1)',
                            'rgba(255, 99, 97, 1)',
                            'rgba(255, 133, 49, 1)',
                            'rgba(255, 166, 0, 1)'
                        ],
                        borderWidth: 1,
                        hoverOffset: 4
                    }
                ]
            },
            options: {
                responsive: true
            }
        });
}
displayCharts();