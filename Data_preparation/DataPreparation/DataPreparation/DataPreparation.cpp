#include <iostream>
#include <locale>
#include <string>
#include <fstream>
#include <map>

bool isEqual(double a, double b) {
    double epsilon = 1e-6;

    return std::abs(a - b) <= epsilon;
}

double F(double x) {
    if ((!isEqual(x, -5) && x > -5) && (x < -3 && !isEqual(x,-3))) {
        return (1. / 8.) * std::sqrt(std::abs(std::sin(3 * x))) * std::cbrt(std::exp(0.15 * x));
    }
    else if ((x < -5 && (x > -7 && !isEqual(x, -7))) || isEqual(x, -5)) {
        return std::pow(x, 20);
    }
    else {
        return std::pow(x, -20) - 10;
    }
}

std::map<std::string, std::pair<double, double>> inputCMD(int count, char* argv[]) {

    std::map<std::string, std::pair<double, double>> fileDataMap;

    for (int i = 1; i < count; i+=3) {
        std::string fileName = argv[i];
        double lower = std::stod(argv[i + 1]);
        double upper = std::stod(argv[i + 2]);

        if (lower > upper)
            std::swap(lower, upper);

        fileDataMap[fileName] = std::make_pair(lower, upper);
    }

    return fileDataMap;
}

void dataGraph(std::ofstream& outputFile, double lower, double upper) {
    double step = lower;
    while (!isEqual(step, upper + 0.05)) {

        if (isEqual(step, -3))
            outputFile << "gap\n";

        if (!isEqual(step, 0))
            outputFile << step << " " << F(step) << "\n";
        else
            outputFile << "gap\n";

        if (isEqual(step, -5) || isEqual(step, -7))
            outputFile << "gap\n";

        step += 0.05;
    }
}

void preparation(std::map<std::string, std::pair<double, double>> fileDataMap, std::string saveDir) {
    for (const auto& entry : fileDataMap) {
        std::string filePath = saveDir + entry.first;
        std::ofstream outputFile(filePath);

        if (!outputFile)
            std::cerr << "Error open file\n";
        else {
            outputFile.imbue(std::locale(""));
            dataGraph(outputFile, entry.second.first, entry.second.second);
        }

        outputFile.close();
    }
}

int main(int argc, char* argv[]) {
    
    std::string saveDir = "../../../../data/";
    
    int count = (argc-1) % 3 == 0 ? argc : argc - ((argc-1) % 3);

    if (count != 1)
        preparation(inputCMD(count, argv), saveDir);
    else 
        std::cerr << "Error input CMD\n";
}
