import sys

if __name__ == "__main__":
    with open(sys.argv[1]) as f:
        lines = f.readlines()
        print(lines[1])