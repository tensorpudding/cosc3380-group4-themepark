import pandas as pd
import numpy as np
import datetime
import pyodbc

NUM_TICKETS=int(1e6)

def softmax(arr):
    return np.exp(arr)/np.sum(np.exp(arr))


def random_ticket_generator():
    # week 20
    start_date=datetime.datetime(2022, 5, 9)
    # week 41
    end_date = datetime.datetime(2022, 10, 9)
    date_range = (end_date - start_date).days

    day_of_the_week_bias = [0.8, 0.6, 0.5, 0.5, 0.6, 0.9, 1.0]
    week_of_the_season_bias = [0.4, 0.3, 0.5, 0.7, 0.8, 0.8, 0.9, 1.0, 1.0, 0.9, 0.9, 0.7, 0.8, 0.6, 0.6, 0.4, 0.5, 0.4, 0.5, 0.3, 0.4, 0.5]
    holiday_bias = 1.2

    dates = [start_date + datetime.timedelta(days=x) for x in range(date_range)]
    holidays = [datetime.datetime(2022, 7, 4)]
    prob = []
    for i in dates:
        day_of_the_week = i.weekday()
        week_of_the_season = int((i-start_date).days / 7)
        holiday_modifier = holiday_bias if i in holidays else 1
        prob.append(day_of_the_week_bias[day_of_the_week] * week_of_the_season_bias[week_of_the_season] * holiday_modifier)
    prob = softmax(np.array(prob))

    ticket_dates = np.random.choice(dates, size=NUM_TICKETS, replace=True, p=prob)
    ticket_classes = np.random.choice(['Poor','Good','Premium'], size=NUM_TICKETS, replace=True)
    ticket_price = 11.30
    ticket_prices = ticket_price * np.ones((NUM_TICKETS,))
    for i in range(NUM_TICKETS):
        yield (ticket_dates[i], ticket_classes[i], ticket_prices[i])

def main():
    # TODO create sql cursor here
    cursor = None

    for ticket in random_ticket_generator():
        cursor.execute('INSERT INTO [Theme_Park].[Ticket] (Date, Ticket_Class, Price) VALUES (' + 
            ticket[0] + 
            ', ' + 
            ticket[1] +
            ', ' +
            ticket[2] +
            ');')
    # TODO commit to database
    conn.commit()

if __name__=='__main__':
    main()