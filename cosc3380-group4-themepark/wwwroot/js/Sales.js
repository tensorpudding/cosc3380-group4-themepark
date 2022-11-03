const displayCharts = () => {
    let _ = null;
    const mainChart1 = document.getElementById("main1-chart").getContext("2d");
    const mainChart2 = document.getElementById("main2-chart").getContext("2d");
    const subChart1 = document.getElementById("sub1-chart").getContext("2d");
    const subChart2 = document.getElementById("sub2-chart").getContext("2d");

    const ticketSalesAry = document.currentScript.getAttribute("ticketSales").split(",").map(obj => { return parseInt(obj) });
    const ticketSalesIncomeAry = document.currentScript.getAttribute("ticketSalesIncome").split(",").map(obj => { return parseFloat(obj) })
    const foodSalesIncomeAry = document.currentScript.getAttribute("foodSalesIncome").split(",").map(obj => { return parseFloat(obj) })
    const merchSalesIncomeAry = document.currentScript.getAttribute("merchSalesIncome").split(",").map(obj => { return parseFloat(obj) })
    let totalIncomeAry = new Array(12);
    for (let i = 0; i < totalIncomeAry.length; i++) {
        totalIncomeAry[i] = ticketSalesIncomeAry[i] + foodSalesIncomeAry[i] + merchSalesIncomeAry[i]
    }


    // Chart 1 Stuff
    _ = new Chart(mainChart1, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'April', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', "Dec"],
            datasets: [
                {
                    label: 'Total Income',
                    data: totalIncomeAry,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(0, 255, 0, 0.5)',
                    borderWidth: 1,
                    tension: 0.2
                },
                {
                    label: 'Ticket Income',
                    data: ticketSalesIncomeAry,
                    backgroundColor: 'rgba(238, 1, 5, 0.2)',
                    borderColor: 'rgba(238, 1, 5, 0.2)',
                    borderWidth: 1,
                    tension: 0.2
                },
                {
                    label: 'Food Income',
                    data: foodSalesIncomeAry,
                    backgroundColor: 'rgb(234,182,118)',
                    borderColor: 'rgb(234,182,118)',
                    borderWidth: 1,
                    tension: 0.2
                },
                {
                    label: 'Merch Income',
                    data: merchSalesIncomeAry,
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 0.2)',
                    borderWidth: 1,
                    tension: 0.2
            }]
        },
        options: {
            plugins: {
                legend: {
                    position: "bottom",
                },
                title: {
                    display: true,
                    text: document.currentScript.getAttribute('year') + ' Annual Earnings',
                    align: "start",
                    font: {
                        size: "24px"
                    }
                }
            },
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    grid: {
                        drawTicks: false,
                        borderDash: [2, 5],
                        drawBorder: false
                    },
                    ticks: {
                        maxTicksLimit: 10
                    }
                },
                x: {
                    grid: {
                        display: false,
                        drawBorder: false
                    }
                }
            }
        }
    });
    // Chart 2 Stuff
    _ = new Chart(mainChart2, {
        type: 'doughnut',
        data: {
            labels: ['Ticket', 'Food', 'Merch'],
            datasets: [{
                label: 'Revenue Split Dataset',
                data: [sum(ticketSalesIncomeAry), sum(foodSalesIncomeAry), sum(merchSalesIncomeAry)],
                backgroundColor: [
                    'rgba(238, 1, 5, 0.2)',
                    'rgb(234,182,118)',
                    'rgba(54, 162, 235, 0.2)',
                ],
                borderColor: [
                    'rgba(238, 1, 5, 0.2)',
                    'rgb(234,182,118)',
                    'rgba(54, 162, 235, 0.2)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            plugins: {
                legend: {
                    position: "bottom",
                },
                title: {
                    display: true,
                    text: document.currentScript.getAttribute('year') + ' Annual Revenue Split',
                    align: "center",
                    font: {
                        size: "24px"
                    }
                }
            },
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    grid: {
                        display: false,
                        drawBorder: false
                    },
                    ticks: {
                        display: false
                    }
                }
            }
        }
    });
}

const sum = (ary) => {
    total = 0;
    ary.forEach(obj => {
        total += obj
    })
    return total;
}

displayCharts();