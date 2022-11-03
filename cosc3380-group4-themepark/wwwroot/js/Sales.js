const displayCharts = () => {
    let _ = null;
    const mainChart1 = document.getElementById("main1-chart").getContext("2d");
    const mainChart2 = document.getElementById("main2-chart").getContext("2d");
    const subChart1 = document.getElementById("sub1-chart").getContext("2d");
    const subChart2 = document.getElementById("sub2-chart").getContext("2d");

    const ticketSalesAry = document.currentScript.getAttribute("ticketSales").split(",").map(obj => { return parseInt(obj) });
    const ticketSalesIncomeAry = document.currentScript.getAttribute("ticketSalesIncome").split(",").map(obj => { return parseFloat(obj) })
    // Chart 1 Stuff
    _ = new Chart(mainChart1, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'April', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', "Dec"],
            datasets: [{
                label: 'Ticket Income',
                data: ticketSalesIncomeAry,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
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
                    text: 'Annual Earnings',
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
            labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
            datasets: [{
                label: '# of Votes',
                data: [12, 19, 3, 5, 2, 3],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
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
                    text: 'Revenue Split',
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

const preventReloadOnFormSubmit = () => {
    const form = document.getElementById("yearSelection");
    function handleForm(event) { event.preventDefault(); }
    form.addEventListener('submit', handleForm);
}

//preventReloadOnFormSubmit();
displayCharts();