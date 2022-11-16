import pandas as pd
import numpy as np
import datetime
import random
import pyodbc

# NUM_TICKETS=int(1e6)-10)
#NUM_TICKETS = 10000
#NUM_SALES = 5000
NUM_TICKETS=1000
NUM_SALES=1000


def softmax(arr):
    return np.exp(arr)/np.sum(np.exp(arr))

def get_merch(cursor):
    probabilities = []
    prices = []
    ids = []
    type_bias = {'Clothing': 0.4, 'Knickknack': 0.7, 'Other': 0.1, 'Food': 0.9, 'Memorabilia': 0.7, 'Picture': 0.2}
    i = 0
    for row in cursor.execute('SELECT item_id, price, merch_type from [Theme_Park].[Merchandise]'):
        ids.append(row.item_id)
        probabilities.append(type_bias[row.merch_type] / np.log(float(row.price)))
        prices.append(row.price)
    return np.array(softmax(probabilities)), prices, ids

def get_rides(cursor):
    ids = []
    probs = []
    popularity = {'Spinny Spin': 0.3, 'Terror Drop': 0.8, 'Magnus XLR': 0.5, 'Pirate Ship': 0.4, 'Dragon Dance': 0.7,
                    'Red Baron': 0.6, 'Antman: The Ride': 0.9, 'Dodgem Cars': 0.5, 'Go Karts': 0.2 }
    for row in cursor.execute('SELECT attraction_id, name from [Theme_Park].[Attractions] where dept_id=1'):
        ids.append(row.attraction_id)
        probs.append(popularity[row.name])
    return np.array(softmax(probs)), ids


def random_data_generator(beginYear = 2022, endYear = 2022, cursor = None):
    start_date=datetime.datetime(random.randint(beginYear, endYear), 5, 9)
    end_date = datetime.datetime(random.randint(beginYear, endYear), 10, 9)
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

    merch_purchase_dates = np.random.choice(dates, size=NUM_SALES, replace=True, p=prob)
    visit_dates = np.random.choice(dates, size=NUM_TICKETS, replace=True, p=prob)
    ticket_classes = np.random.choice(['Poor','Normal','Premium'], size=NUM_TICKETS, replace=True)
    ticket_prices = {'Poor': 11.50, 'Normal': 13.00, 'Premium': 16.00 }
    merch_prob, merch_prices, merch_ids = get_merch(cursor)
    attraction_prob, attraction_ids = get_rides(cursor)

    for i in range(NUM_SALES):
        purchase_item = np.random.choice(list(range(len(merch_ids))), size=1, p=merch_prob)[0]
        cursor.execute('INSERT INTO [Theme_Park].[Merchandise_Sale] (item_id, quantity, sale_price, transaction_date) Values ({}, 1, {}, \'{}\')'.format(
            merch_ids[purchase_item], 
            merch_prices[purchase_item], 
            merch_purchase_dates[i]
        ))
        if i % 100 == 0 and i != 0:
            print(f"{i}/{NUM_SALES}")
            cursor.commit()
    print("COMPLETED MERCH")
    for i in range(NUM_TICKETS):
        sql = "INSERT INTO [Theme_Park].[Ticket] (Date, Ticket_Class, Price) VALUES ('{}', '{}', {})".format(
            #visit_dates[i].strftime("%Y%m%d %H:%M:%S"), 
            visit_dates[i].strftime("%Y-%m-%d"),
            ticket_classes[i], 
            ticket_prices[ticket_classes[i]]
        )
        cursor.execute(sql)
        attraction_ridden = np.random.choice(list(range(len(attraction_ids))), size=np.random.random_integers(1, 7), p=attraction_prob)
        for id in attraction_ridden:
            sql = "INSERT INTO [Theme_Park].[Attractions_Visit] (visit_time, attractions_id) VALUES ('{}', {})".format(
                visit_dates[i].strftime("%Y-%m-%d"),
                attraction_ids[id],
            )
            cursor.execute(sql)
        if i % 100 == 0 and i != 0:
            print(f"{i}/{NUM_SALES}")
            cursor.commit()
    print("COMPLETED TICKETS/RIDES")

def main():
    # TODO create sql cursor here
    server = "cosc3380group4.moorman.xyz"
    database = "themepark_dev"
    username = "phillip"
    password = "{koch#frock59flush}"
    driver = "{/opt/homebrew/lib/libmsodbcsql.17.dylib}" #I found the db driver doing 'odbcinst -j' in cli and opening odbcinst.ini
    #driver = "{FreeTDS}"

    conn_string = ("DRIVER={}; SERVER=tcp:{}; PORT=1433; DATABASE={}; UID={}; PWD={}").format(driver, server, database, username, password)
    cnxn = pyodbc.connect(conn_string)
    cursor = cnxn.cursor()

    #random_ticket_generator(2020, 2020, cursor)
    random_data_generator(2020, 2020, cursor)
    random_data_generator(2021, 2021, cursor)
    random_data_generator(2022, 2022, cursor)
    # probs, ids = get_rides(cursor)
    # for i in range(len(probs)):
    #     print(f"Attraction {ids[i]} has prob {probs[i]}")

    cnxn.commit()
main()
