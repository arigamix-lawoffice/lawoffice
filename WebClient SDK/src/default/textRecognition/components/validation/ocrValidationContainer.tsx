import FontIcon from 'ui/icons/fontIcon';
import React from 'react';
import { getTessaIcon } from 'common';
import { observer } from 'mobx-react';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';
import './ocrValidationContainerStyle.css';

/** Пропсы контрола-контейнера для отображения результата проверки введенных значений. */
interface IOcrValidationContainerProps {
  /** Признак того, что выполняется загрузка данных. */
  isLoadingEnabled?: boolean;
  /** Результат валидации. */
  validationResult: ValidationResult | null;
}

/** Контрол-контейнер для отображения результата проверки введенных значений. */
export const OcrValidationContainer: React.FC<IOcrValidationContainerProps> = observer(
  ({ isLoadingEnabled, validationResult, children }) => {
    const icon = React.useMemo<JSX.Element>(() => {
      if (isLoadingEnabled) {
        return <FontIcon className={'fa fa-spinner fa-spin fa-fw'} />;
      } else if (!validationResult || validationResult.items.length === 0) {
        return <FontIcon className={getTessaIcon('Int426')} style={{ color: 'green' }} />;
      }

      let hasWarning = false;
      for (const item of validationResult.items) {
        if (item.type === ValidationResultType.Error) {
          return <FontIcon className={getTessaIcon('Int352')} style={{ color: 'red' }} />;
        } else if (!hasWarning && item.type === ValidationResultType.Warning) {
          hasWarning = true;
        }
      }

      if (hasWarning) {
        return <FontIcon className={getTessaIcon('Int202')} style={{ color: 'orange' }} />;
      } else {
        return <FontIcon className={getTessaIcon('Int204')} style={{ color: 'gray' }} />;
      }
    }, [isLoadingEnabled, validationResult]);

    return (
      <div className="validation-container">
        <div className="content">
          {children}
          {icon}
        </div>
        <div className="message-container">
          {validationResult?.items.map((item, index) => (
            <span key={index} className="message" style={getMessageStyle(item.type)}>
              {item.message}
            </span>
          ))}
        </div>
      </div>
    );
  }
);

function getMessageStyle(messageType: ValidationResultType): React.CSSProperties {
  switch (messageType) {
    case ValidationResultType.Error:
      return { backgroundColor: 'rgba(255, 100, 70, 1)', color: 'white' };
    case ValidationResultType.Warning:
      return { backgroundColor: 'rgba(250, 255, 175, 1)', color: 'black' };
    case ValidationResultType.Info:
      return { backgroundColor: 'rgba(150, 150, 150, 1)', color: 'white' };
    default:
      return { backgroundColor: 'rgba(150, 255, 150, 1)', color: 'black' };
  }
}
