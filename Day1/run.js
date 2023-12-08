const fs = require('fs');

function getData() {
    let data = fs.readFileSync('./input.txt', 'utf8').split('\n');
    return data;
}

function part1(data) {
    console.log(
        data.map((line) => {
            if (line === '') return 0;

            const decimals = line.match(/[0-9]/g);

            if (decimals === null) return 0;

            if (decimals.length === 1) {
                return `${decimals[0]}${decimals[0]}`;
            }

            return `${decimals[0]}${decimals[decimals.length - 1]}`;
        }).reduce((acc, curr) => acc + parseInt(curr), 0)
    );
}

function part2(data) {
    part1(
        data.map((line) => {
            return line.replaceAll("twone", "21")
                .replaceAll("oneight", "18")
                .replaceAll("eightwo", "82")
                .replaceAll("eighthree", "83")
                .replaceAll("nineight", "98")
                .replaceAll("sevenine", "79")
                .replaceAll("fiveight", "58")
                .replaceAll("threeight", "38")
                .replaceAll("one", "1")
                .replaceAll("two", "2")
                .replaceAll("three", "3")
                .replaceAll("four", "4")
                .replaceAll("five", "5")
                .replaceAll("six", "6")
                .replaceAll("seven", "7")
                .replaceAll("eight", "8")
                .replaceAll("nine", "9")
        })
    )
}

console.log("Part 1:");
part1(getData());
console.log("Part 2:");
part2(getData());
