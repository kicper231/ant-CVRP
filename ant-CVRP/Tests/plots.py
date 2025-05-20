# Chat gtp generated
import pandas as pd
import matplotlib.pyplot as plt


minmax_df = pd.read_csv("Results/minMax_results_3_n51-k3_1.csv")
elite_df = pd.read_csv("Results/elite_n51.csv")

minmax_avg = minmax_df[minmax_df["Run"] == "avg"].copy()
elite_avg = elite_df[elite_df["Run"] == "avg"].copy()

minmax_avg["Min"] = minmax_avg["Min"].astype(float)
minmax_avg["Max"] = minmax_avg["Max"].astype(int)
minmax_avg["Rating"] = minmax_avg["Rating"].astype(float)

elite_avg["EliteAntCount"] = elite_avg["EliteAntCount"].astype(int)
elite_avg["Rating"] = elite_avg["Rating"].astype(float)/2

# Wartość oczekiwana
expected_rating = 521

# Wykres Min/Max
plt.figure(figsize=(10, 6))
for min_val in minmax_avg["Min"].unique():
    subset = minmax_avg[minmax_avg["Min"] == min_val]
    plt.plot(subset["Max"], subset["Rating"], marker="o", label=f"Min={min_val}")
plt.axhline(expected_rating, color="red", linestyle="--", label="Oczekiwana długość (521)")
plt.xlabel("Parametr Max")
plt.ylabel("Średnia długość trasy")
plt.title("Wyniki dla róznych wartości parametrów Min i Max dla instancji E-n51-k5")
plt.legend()
plt.grid(True)
plt.tight_layout()
plt.show()


# Wykres EliteAntCount
plt.figure(figsize=(8, 5))
plt.plot(
    elite_avg["EliteAntCount"], elite_avg["Rating"], marker="o", label="Liczba elitarnych mrówek"
)
plt.axhline(expected_rating, color="red", linestyle="--", label="Oczekiwana długość (521)")
plt.xlabel("Liczba elitarnych mrówek")
plt.ylabel("Średnia długość trasy")
plt.title("Średnie wyniki da róznych liczby elitarnych mrówek")
plt.legend()
plt.grid(True)
plt.tight_layout()
plt.show()

def preprocess(df):
    df = df[pd.to_numeric(df["Run"], errors="coerce").notnull()].copy()
    df["Run"] = df["Run"].astype(int)
    df["Rating"] = df["Rating"].astype(float)
    df_ant = df[df["Algorithm"] == "Ant"]

    # Sprawdzenie istnienia kolumny Iter
    if "Iter" not in df_ant.columns:
        raise ValueError("Brakuje kolumny 'Iter' w danych.")

    return df_ant.groupby("Iter")["Rating"].mean()


df_true = pd.read_csv("Results/compare_n33_elite_true.csv")
df_false = pd.read_csv("Results/compare_n33_minMax.csv")

avg_true = preprocess(df_true)
avg_false = preprocess(df_false)

greedy_value = 1010
expected_value = 835

plt.figure(figsize=(12, 6))
plt.plot(avg_true.index, avg_true.values, label="ACO podstawowy", linewidth=2)
plt.plot(avg_false.index, avg_false.values, label="ACO Min-Max", linewidth=2)

plt.axhline(greedy_value, color="green", linestyle="--", label="Zachłanny (1010)")
plt.axhline(expected_value, color="red", linestyle="--", label="Optymalny (835)")

plt.ylim(800, 1100)
plt.xlabel("Iteracja")
plt.ylabel("Ocena")
plt.title("Wykres zbieżności algorytmu mrówkowego\n(Instancja: E-n33-k4)")
plt.legend()
plt.grid(True)
plt.tight_layout()
plt.show()