import os

# Функция для изменения кодировки файла
def convert_encoding(file_path, src_encoding='windows-1251', dest_encoding='utf-8'):
    with open(file_path, 'r', encoding=src_encoding) as f:
        content = f.read()

    with open(file_path, 'w', encoding=dest_encoding) as f:
        f.write(content)

# Рекурсивно ищем все файлы в папке с расширением .cs
def convert_folder_encoding(folder_path, src_encoding='windows-1251', dest_encoding='utf-8'):
    for dirpath, _, filenames in os.walk(folder_path):
        for filename in filenames:
            file_path = os.path.join(dirpath, filename)
            # Проверяем, что файл имеет расширение .cs
            if filename.endswith('.cs'):
                try:
                    convert_encoding(file_path, src_encoding, dest_encoding)
                    print(f"Файл {file_path} преобразован в {dest_encoding}")
                except Exception as e:
                    print(f"Ошибка при обработке {file_path}: {e}")

# Указываем путь к папке с вашими скриптами
folder_path = r'C:\Users\sokol\Documents\GitHub\SlimeAdventure\Assets\Scripts'
convert_folder_encoding(folder_path)
